using MaintenanceManager.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.MaintenanceRule
{
    public class MaintenanceRuleResponseDto
    {
        public required string Reference { get; set; }
        public required string RuleName { get; set; }
        public required CounterType CounterType { get; set; }
        public required int IntervalValue { get; set; }
        public string? Description { get; set; }
        public required ComponentType AppliesTo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
