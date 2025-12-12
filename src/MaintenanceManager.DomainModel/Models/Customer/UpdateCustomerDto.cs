using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.Customers
{
    public class UpdateCustomerDto
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
