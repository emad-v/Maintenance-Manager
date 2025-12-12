using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Models.UsageCounter;
using MaintenanceManager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Business
{
    public class UsageCounterService
    {
        private readonly IUsageCounterRepository _counterRepository;
        private readonly ComponentRuleStatusService _ruleStatusService;


        public UsageCounterService(IUsageCounterRepository repository, ComponentRuleStatusService componentRuleStatusService)
        {
            _counterRepository = repository;
            _ruleStatusService = componentRuleStatusService;

        }


        public async Task<UpdateUsageCounterResponseDto> IncrementCounter(string counterReference, int incrementValue)
        {

            if (!(await _counterRepository.ReferenceExistAsync(counterReference)))
            {
                throw new KeyNotFoundException($"Counter with reference: {counterReference} was not found");
            }

            if (incrementValue <= 0)
            {
                throw new InvalidOperationException("Increment value must be positive");
            }
            UsageCounter updatedCounter = await _counterRepository.IncrementCounter(counterReference, incrementValue);

            await _ruleStatusService.ProcessOverdueStatuses(updatedCounter);

            return new UpdateUsageCounterResponseDto()
            {
                Reference = updatedCounter.Reference,
                ComponentReference = updatedCounter.ComponentReference,
                CounterType = updatedCounter.CounterType,
                Value = updatedCounter.Value,
                UpdatedAt = updatedCounter.UpdatedAt,

            };
        }

    }
}
