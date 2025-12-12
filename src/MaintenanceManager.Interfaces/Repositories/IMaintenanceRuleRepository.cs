using MaintenanceManager.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Interfaces.Repositories
{
    public interface IMaintenanceRuleRepository
    {
        Task<MaintenanceRule> AddMaintenanceRule(MaintenanceRule maintenanceRule);
        Task<MaintenanceRule> GetMaintenanceRuleByReference(string reference);
        Task<IEnumerable<MaintenanceRule>> GetMaintenanceRules();
        Task<bool> ReferenceExistAsync(string reference);
    }
}
