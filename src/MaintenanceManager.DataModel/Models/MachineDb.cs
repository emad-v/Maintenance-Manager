using MaintenanceManager.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Models
{
    public class MachineDb
    {
        public int Id { get; set; }
        public required string Reference { get; set; }
        public required string Name { get; set; }
        public required string Model { get; set; }
        public required MachineType Type { get; set; }
        public required string CustomerReference { get; set; }
        public DateTime CreatedDate { get; set; }

    }

}
