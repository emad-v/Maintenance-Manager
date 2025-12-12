using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.UserCustomerLink
{
    public class LinkUserDto
    {
        public required string AdminRef {  get; set; }
        public required string UserRef { get; set; }

    }
}
