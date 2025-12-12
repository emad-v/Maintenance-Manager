using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Models
{
    [DynamoDBTable("MaintenanceLogTable")]
    public class MaintenanceLogDynamoDb
    {
        [DynamoDBHashKey("PK")]
        public required string PK { get; set; } // it would be "STATUS#{StatusReference}" 
        [DynamoDBRangeKey("SK")]
        public required string SK { get; set; } //" LOG#{TimeStamp} 
        public string Reference { get; set; } // 
        public required string StatusReference { get; set; }
        public required string PerformedBy { get; set; }
        public string WorkPerformed { get; set; }
        public DateTime CompletedAt { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
