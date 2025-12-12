using MaintenanceManager.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Entities
{
    public class Machine
    {
        public required string Reference { get; set; }
        public required string Name { get; set; }
        public required string Model { get; set; }
        public required MachineType Type { get; set; }
        public required string CustomerReference { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
