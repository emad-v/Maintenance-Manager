using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Helpers
{
    public static class ReferenceGenerator
    {
       

        public static string GenerateComponentReference()
            => $"COMP-{Guid.NewGuid()}";

        public static string GenerateCounterReference()
            => $"COUNTER-{Guid.NewGuid()}";

        public static string GenerateStatusReference()
            => $"STATUS-{Guid.NewGuid()}";

        public static string GenerateLogReference()
            => $"LOG-{Guid.NewGuid()}";
        public static string GenerateNotificationReference()
            => $"NOTIF-{Guid.NewGuid()}";

        public static string GenerateUserReference()
            => $"USR-{Guid.NewGuid()}";
        public static string GenerateUserCustomerReference()
            => $"USR-CUS-{Guid.NewGuid()}";

     
       




    }
}
