using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Entities
{
    public class UserCustomer
    {
        public string? Reference { get; set; }
        public required string UserReference { get; set; }
        public required string CustomerReference { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
