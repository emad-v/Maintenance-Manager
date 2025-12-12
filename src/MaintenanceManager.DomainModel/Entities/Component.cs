using MaintenanceManager.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Entities
{
    public class Component
    {
        public required string Reference { get; set; }
        public required string MachineReference { get; set; }
        public required string Name { get; set; }
        public required ComponentType Type { get; set; }
        public string? SerialNo { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
