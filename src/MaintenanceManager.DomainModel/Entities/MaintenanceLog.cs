using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Entities
{
    public class MaintenanceLog
    {
        public required string Reference { get; set; }
        public required string StatusReference { get; set; }
        public required string PerformedBy { get; set; }
        public string? WorkPerformed { get; set; }
        public DateTime CompletedAt { get; set; }    // when the actual maintenance work was performed (business event)
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } //  when the record was saved to database (technical detail) 

    }
}
