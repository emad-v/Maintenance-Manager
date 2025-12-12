using MaintenanceManager.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Entities
{
    public class UsageCounter
    {
        public  required string Reference { get; set; }
        public required string ComponentReference { get; set; }
        public required CounterType CounterType { get; set; }
        public required int Value { get; set; }
        public required DateTime UpdatedAt { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
