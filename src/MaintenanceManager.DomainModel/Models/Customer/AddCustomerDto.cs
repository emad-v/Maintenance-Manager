using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.Customers
{
    public class AddCustomerDto
    {
        public required string Reference { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

    }
}
