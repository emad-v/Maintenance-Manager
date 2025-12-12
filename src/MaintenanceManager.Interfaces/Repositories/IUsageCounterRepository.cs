using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Interfaces.Repositories
{
    public interface IUsageCounterRepository
    {
        Task<UsageCounter>AddCounter(UsageCounter counter);
        Task<UsageCounter?>GetCounterTypeForComponent(string componentReference, CounterType type);
        Task<UsageCounter> IncrementCounter(string counterReference, int incrementAmount);
        Task<bool> ReferenceExistAsync(string reference);
        Task<UsageCounter> GetCounterByReference(string reference);


    }
}
