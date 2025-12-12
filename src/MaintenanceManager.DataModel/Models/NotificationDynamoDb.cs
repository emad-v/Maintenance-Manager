using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Models
{
    [DynamoDBTable("NotificationTable")]
    public class NotificationDynamoDb
    {
        [DynamoDBHashKey("PK")]
        public required string PK { get; set; }// it would be "USER#{recipientReference}
        [DynamoDBRangeKey("SK")]
        public required string SK { get; set; } // "NOTIF #{timeStamp}  notification.SK = $"NOTIF#{notification.created at:yyyyMMddHHmmss}";
        public required string Reference { get; set; }
        public required string RecipientReference { get; set; }
        public required string Message { get; set; }
        public required string StatusReference { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
