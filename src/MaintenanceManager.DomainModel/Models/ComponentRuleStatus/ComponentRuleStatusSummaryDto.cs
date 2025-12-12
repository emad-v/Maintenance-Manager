using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.ComponentRuleStatus
{
    public class ComponentRuleStatusSummaryDto
    {
        public string? Reference { get; set; }
        public required string ComponentReference { get; set; }
        public required string MaintenanceRuleReference { get; set; }
        public required string UsageCounterReference { get; set; }
        public required DateTime LastServiceAt { get; set; }
       

    }
}
