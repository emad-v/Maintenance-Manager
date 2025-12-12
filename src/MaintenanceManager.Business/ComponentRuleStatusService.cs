using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Enums;
using MaintenanceManager.DomainModel.Helpers;
using MaintenanceManager.DomainModel.Models.ComponentRuleStatus;
using MaintenanceManager.DomainModel.Models.UsageCounter;
using MaintenanceManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;


namespace MaintenanceManager.Business
{
    public class ComponentRuleStatusService
    {
       
        private readonly IComponentRuleStatusRepository _statusReposiotry;
        private readonly IComponentRepository _componentRepository;
        private readonly IMaintenanceRuleRepository _maintenanceRuleRepository;
        private readonly IUsageCounterRepository _usageCounterRepository;
        private readonly NotificationService _notificationService;

        public ComponentRuleStatusService(IComponentRuleStatusRepository statusReposiotry, IComponentRepository componentRepository, IMaintenanceRuleRepository maintenanceRuleRepository, IUsageCounterRepository usageCounter,NotificationService notificationService)
        {
            _statusReposiotry = statusReposiotry;
            _componentRepository = componentRepository; 
            _maintenanceRuleRepository = maintenanceRuleRepository;
            _usageCounterRepository = usageCounter;
            _notificationService = notificationService;
        }


        public async Task<ComponentRuleStatusResponse> AssignRule(string componentRef,string maintenanceRuleReference)
        {
            Component component = await _componentRepository.GetComponentByReference(componentRef);
            MaintenanceRule maintenanceRule = await _maintenanceRuleRepository.GetMaintenanceRuleByReference(maintenanceRuleReference);

            if (!(maintenanceRule.AppliesTo.Equals(component.Type)))
            {
                throw new InvalidOperationException($"The maintenance Type :{maintenanceRule.AppliesTo} doesnt match the component type {component.Type} ");
            }

            UsageCounter? usageCounter = await _usageCounterRepository.GetCounterTypeForComponent(component.Reference, maintenanceRule.CounterType);

            if (usageCounter == null)
            {
                usageCounter = new UsageCounter()
                {
                    Reference = ReferenceGenerator.GenerateCounterReference(),
                    ComponentReference = component.Reference,
                    CounterType = maintenanceRule.CounterType,
                    Value = 0,
                    UpdatedAt = DateTime.UtcNow
                };
                 await _usageCounterRepository.AddCounter(usageCounter);
            }
            if (await _statusReposiotry.ExistsAsync(component.Reference, maintenanceRule.Reference))
            {
                throw new InvalidDataException($"Rule is already assigned to this component");
            }
            ComponentRuleStatus componentRuleStatus = new ComponentRuleStatus()
            {
                Reference = ReferenceGenerator.GenerateStatusReference(),
                ComponentReference = component.Reference,
                MaintenanceRuleReference = maintenanceRule.Reference,
                UsageCounterReference = usageCounter.Reference,
                LastServiceAt = DateTime.UtcNow,
                NextDueIn = maintenanceRule.IntervalValue,
                IsOverDue = usageCounter.Value >= maintenanceRule.IntervalValue
            };

             await _statusReposiotry.AddStatus(componentRuleStatus);

            return new ComponentRuleStatusResponse()
            {
                Reference = componentRuleStatus.Reference,
                ComponentReference = componentRuleStatus.ComponentReference,
                MaintenanceRuleReference = componentRuleStatus.MaintenanceRuleReference,
                UsageCounterReference = componentRuleStatus.UsageCounterReference,
                LastServiceAt = componentRuleStatus.LastServiceAt,
                CurrentUsage = usageCounter.Value,
                Remaining = maintenanceRule.IntervalValue - usageCounter.Value,
                ThresholdPercentage = maintenanceRule.IntervalValue > 0 ? ((double)usageCounter.Value / maintenanceRule.IntervalValue) * 100 : 0,
                IsOverDue = componentRuleStatus.IsOverDue,
                CreatedDate = componentRuleStatus.CreatedDate,

            };
        }

        public async Task<IEnumerable<ComponentRuleStatusSummaryDto>> ListComponentStatuses(string componentReference)
        {
            var statuses = await _statusReposiotry.GetAllStatusForComponent(componentReference);

            var result = statuses.Select(c => new ComponentRuleStatusSummaryDto()
            {

                Reference = c.Reference,
                ComponentReference = c.ComponentReference,
                MaintenanceRuleReference = c.MaintenanceRuleReference,
                UsageCounterReference = c.UsageCounterReference,
                LastServiceAt = c.LastServiceAt,
            }
            );
            return result;
        }


        public async Task<ComponentRuleStatusResponse> GetStatusByReference(string componentReference, string statusReference)
        {

            ComponentRuleStatus componentRuleStatus = await _statusReposiotry.GetComponentRuleStatusByReference(statusReference);
            MaintenanceRule maintenanceRule = await _maintenanceRuleRepository.GetMaintenanceRuleByReference(componentRuleStatus.MaintenanceRuleReference);
            UsageCounter? usageCounter = await _usageCounterRepository.GetCounterTypeForComponent(componentReference, maintenanceRule.CounterType);
            if (usageCounter == null)
            {
                throw new InvalidOperationException("The Usage counter cant be null");
            }
            return new ComponentRuleStatusResponse()
            {
                Reference = componentRuleStatus.Reference,
                ComponentReference = componentReference,
                MaintenanceRuleReference = componentRuleStatus.MaintenanceRuleReference,
                UsageCounterReference = componentRuleStatus.UsageCounterReference,
                LastServiceAt = componentRuleStatus.LastServiceAt,
                CurrentUsage = usageCounter.Value,
                Remaining = maintenanceRule.IntervalValue - usageCounter.Value,
                ThresholdPercentage = maintenanceRule.IntervalValue > 0 ? ((double)usageCounter.Value / maintenanceRule.IntervalValue) * 100 : 0,
                IsOverDue = usageCounter.Value >= maintenanceRule.IntervalValue,
              


            };
        }


        public async Task ProcessOverdueStatuses(UsageCounter usageCounter)
        {
            var statuses = await _statusReposiotry.GetAllStatusForCounter(usageCounter.Reference);

            foreach (var status in statuses)
            {
                var MaintenanceRule = await _maintenanceRuleRepository.GetMaintenanceRuleByReference(status.MaintenanceRuleReference);
                if (!status.IsOverDue && usageCounter.Value >= status.LastMaintenanceCounterValue + MaintenanceRule.IntervalValue)
                {
                    await _statusReposiotry.UpdateOverDue(status.Reference,usageCounter.Value);
                    await _notificationService.CreateOverDueNotifications(status.Reference);
                }
               
            }

        }

    }


}






