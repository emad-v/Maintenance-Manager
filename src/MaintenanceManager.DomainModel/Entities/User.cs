using MaintenanceManager.DomainModel.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Entities
{
    public class User
    {
        public required string Reference { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
    }
}
