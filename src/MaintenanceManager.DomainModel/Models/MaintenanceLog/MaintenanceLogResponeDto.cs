using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.MaintenanceLog
{
    public class MaintenanceLogResponeDto
    {
        public required string Reference { get; set; }
        public required string StatusReference { get; set; }
        public required string PerformedBy { get; set; }
        public string? WorkPerformed { get; set; }
        public DateTime CompletedAt { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
