using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.UserCustomerLink
{
    public class LinkUserToCustomerResponseDto
    {
        public required string Reference;
        public required string UserRef;
        public required string CustomerRef;
        public DateTime AssignedAt;

    }
}
