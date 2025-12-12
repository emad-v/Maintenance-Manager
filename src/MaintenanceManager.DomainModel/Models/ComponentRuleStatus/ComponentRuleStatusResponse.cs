using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.ComponentRuleStatus
{
    public class ComponentRuleStatusResponse
    {
        public string? Reference { get; set; }
        public required string ComponentReference { get; set; }
        public required string MaintenanceRuleReference { get; set; }
        public required string UsageCounterReference { get; set; }
        public required DateTime LastServiceAt { get; set; }
        public required int  CurrentUsage { get; set; }
        public required int Remaining { get; set; }
        public required double ThresholdPercentage { get; set; }
        public required bool IsOverDue { get; set; } = false;
        public DateTime CreatedDate { get; set; }
    }
}
