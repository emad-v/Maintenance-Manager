using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Entities
{
    public class ComponentRuleStatus
    {
        public required string Reference { get; set; }
        public required string ComponentReference { get; set; }
        public required string MaintenanceRuleReference { get; set; }
        public required string UsageCounterReference { get; set; }
        public required DateTime LastServiceAt { get; set; }
        public required int NextDueIn { get; set; }
        public required bool IsOverDue { get; set; } = false;
        public int LastMaintenanceCounterValue {  get; set; } 
        public DateTime CreatedDate { get; set; }

    }
}
