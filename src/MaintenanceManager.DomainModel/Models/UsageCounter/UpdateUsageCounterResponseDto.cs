using MaintenanceManager.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.UsageCounter
{
    public class UpdateUsageCounterResponseDto
    {
        public required string Reference { get; set; }
        public required string ComponentReference { get; set; }
        public required CounterType CounterType { get; set; }
        public required int Value { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}
