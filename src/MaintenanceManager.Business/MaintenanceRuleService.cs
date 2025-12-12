using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Enums;
using MaintenanceManager.DomainModel.Models.MaintenanceRule;
using MaintenanceManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Business
{
    public class MaintenanceRuleService
    {
        private readonly IMaintenanceRuleRepository _repository;

        public MaintenanceRuleService(IMaintenanceRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<MaintenanceRuleResponseDto> CreateMaintenanceRule(AddToMaintenanceRuleDto addMaintenanceRuleRequest)
        {
            if (await _repository.ReferenceExistAsync(addMaintenanceRuleRequest.Reference))
            {
                throw new InvalidOperationException($"Maintenance rule with reference {addMaintenanceRuleRequest.Reference} already exist");//409 error
            }

            MaintenanceRule maintenanceRule = new MaintenanceRule()
            {
                Reference = addMaintenanceRuleRequest.Reference,
                RuleName = addMaintenanceRuleRequest.RuleName,
                CounterType = addMaintenanceRuleRequest.CounterType,
                IntervalValue = addMaintenanceRuleRequest.IntervalValue,
                Description = addMaintenanceRuleRequest.Description,
                AppliesTo = addMaintenanceRuleRequest.AppliesTo,
            };
            await _repository.AddMaintenanceRule(maintenanceRule);
            return new MaintenanceRuleResponseDto()
            {
                Reference = maintenanceRule.Reference,
                RuleName = maintenanceRule.RuleName,
                CounterType = maintenanceRule.CounterType,
                IntervalValue = maintenanceRule.IntervalValue,
                Description = maintenanceRule.Description,
                AppliesTo = maintenanceRule.AppliesTo,
                CreatedDate = maintenanceRule.CreatedDate
            };
        }


        public async Task<MaintenanceRuleResponseDto> GetMaintenanceRuleByReference(string reference)
        {
            var maintenanceRule = await _repository.GetMaintenanceRuleByReference(reference);

            return new MaintenanceRuleResponseDto()
            {
                Reference = maintenanceRule.Reference,
                RuleName = maintenanceRule.RuleName,
                CounterType = maintenanceRule.CounterType,
                IntervalValue = maintenanceRule.IntervalValue,
                Description = maintenanceRule.Description,
                AppliesTo = maintenanceRule.AppliesTo,
                CreatedDate = maintenanceRule.CreatedDate
            };
        }

        public async Task<IEnumerable<MaintenanceRuleResponseDto>> GetMaintenanceRules()
        {
            var maintenanceRules = await _repository.GetMaintenanceRules();
            var result = maintenanceRules.Select(m => new MaintenanceRuleResponseDto()
            {
                Reference = m.Reference,
                RuleName = m.RuleName,
                CounterType = m.CounterType,
                IntervalValue = m.IntervalValue,
                Description = m.Description,
                AppliesTo = m.AppliesTo,
                CreatedDate = m.CreatedDate
            });
            return result;

        }
    }
}

