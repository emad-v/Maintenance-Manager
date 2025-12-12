using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Models
{
    public class ComponentRuleStatusDb
    {
        public int Id { get; set; }
        public required string Reference { get; set; }
        public required string ComponentReference { get; set; }
        public required string MaintenanceRuleReference { get; set; }
        public required string UsageCounterReference { get; set; }
        public required DateTime LastServiceAt { get; set; }
        public required int NextDueIn { get; set; }
        public required bool IsOverDue { get; set; }
        public int LastMaintenanceCounterValue { get; set; } = 0;
        public DateTime CreatedDate { get; set; }
    }
}
