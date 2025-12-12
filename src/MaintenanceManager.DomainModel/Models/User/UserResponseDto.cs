using MaintenanceManager.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.User
{
    public class UserResponseDto
    {
        public required string Reference { get; set; }
        public required string Name { get; set; }
        public string Email { get; set; }
        public required UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
    }
}
