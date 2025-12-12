using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.Notification
{
    public class NotificationResponseDto
    {
        public required string Reference { get; set; }
        public required string RecipientReference { get; set; }
        public required string Message { get; set; }
        public required string StatusReference { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
