using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Models
{
    public class UserCustomerDb
    {
        public int Id { get; set; } 
        public string? Reference { get; set; }
        public required string UserReference { get; set; }
        public required string CustomerReference { get; set; }
        public DateTime AssignedAt { get; set; }

    }
}
