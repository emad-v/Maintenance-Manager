using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Helpers;
using MaintenanceManager.DomainModel.Models.MaintenanceLog;
using MaintenanceManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Business
{
    public class MaintenanceLogService
    {
        private readonly IMaintenanceLogRepository _maintenanceLogRepository;
        private readonly IComponentRuleStatusRepository _statusRepository;
        private readonly IUsageCounterRepository _usageCounterRepository;
        private readonly IMaintenanceRuleRepository _maintenanceRuleRepository;

        public MaintenanceLogService(IMaintenanceLogRepository maintenanceLogRepository, IComponentRuleStatusRepository statusRepository, IUsageCounterRepository usageCounterRepository, IMaintenanceRuleRepository maintenanceRuleRepository)
        {
            _maintenanceLogRepository = maintenanceLogRepository;
            _statusRepository = statusRepository;
            _usageCounterRepository = usageCounterRepository;
            _maintenanceRuleRepository = maintenanceRuleRepository;
        }

        public async Task<MaintenanceLogResponeDto> CompleteMaintenance(string statusRef, CompleteMaintenanceDto completeTaskRequest)
        {
            var status = await _statusRepository.GetComponentRuleStatusByReference(statusRef);
            var usageCounter = await _usageCounterRepository.GetCounterByReference(status.UsageCounterReference);

            Console.WriteLine($"Counter value in maintenance log service: {usageCounter.Value}");

            var maintenanceRule = await _maintenanceRuleRepository.GetMaintenanceRuleByReference(status.MaintenanceRuleReference);
            if (!(status.IsOverDue))
            {
                throw new InvalidOperationException("The maintenance is not due yet, you cant perform this task ");
            }
            int nextDueIn = usageCounter.Value + maintenanceRule.IntervalValue;
            DateTime lastServiceAt = DateTime.UtcNow;
            await _statusRepository.UpdateMaintenanceStatus(statusRef, lastServiceAt, nextDueIn, usageCounter.Value);

            MaintenanceLog maintenanceLog = new MaintenanceLog
            {
                Reference = ReferenceGenerator.GenerateLogReference(),
                StatusReference = statusRef,
                PerformedBy = completeTaskRequest.PerformedByRef,
                CompletedAt = lastServiceAt,
                WorkPerformed = completeTaskRequest.WorkPerformed,
                Notes = completeTaskRequest.Notes,
                CreatedAt = DateTime.UtcNow,
            };
            var createdLog = await _maintenanceLogRepository.AddLog(maintenanceLog);

            return new MaintenanceLogResponeDto
            {
                Reference = createdLog.Reference,
                StatusReference = createdLog.StatusReference,
                PerformedBy = createdLog.PerformedBy,
                CompletedAt = createdLog.CompletedAt,
                WorkPerformed = completeTaskRequest.WorkPerformed,
                Notes = completeTaskRequest.Notes,

            };

        }


        public async Task<IEnumerable<MaintenanceLogResponeDto>> GetMaintenanceByStatus(string statusRef)
        {
            var logsDb = await _maintenanceLogRepository.GetLogsByStatus(statusRef);
            return logsDb.Select(l => new MaintenanceLogResponeDto
            {
                Reference = l.Reference,
                StatusReference = l.StatusReference,
                PerformedBy = l.PerformedBy,
                CompletedAt = l.CompletedAt,
                WorkPerformed = l.WorkPerformed,
                Notes = l.Notes,
            });

        }

    }
}
