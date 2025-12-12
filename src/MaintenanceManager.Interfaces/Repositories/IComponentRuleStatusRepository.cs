using MaintenanceManager.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Interfaces.Repositories
{
    public interface IComponentRuleStatusRepository
    {
        Task<ComponentRuleStatus> AddStatus(ComponentRuleStatus componentRuleStatus);
        Task<ComponentRuleStatus> GetComponentRuleStatusByReference(string statusReference);
        Task<IEnumerable<ComponentRuleStatus>> GetAllStatusForComponent(string ComponentReference);
        Task<IEnumerable<ComponentRuleStatus>> GetAllStatusForCounter(string counterRef);
        Task<ComponentRuleStatus> UpdateMaintenanceStatus(String statusReference, DateTime lastServiceAt, int NextDueIn,int usageCounterValue);
        Task UpdateOverDue(string statusRef,int lastVisitedThreshold);
        Task<bool> ExistsAsync(string componentReference, string ruleReference);
        Task DeleteStatus(string statusReference);
    }
}
