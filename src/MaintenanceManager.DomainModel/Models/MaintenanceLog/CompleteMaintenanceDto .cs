using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Models.MaintenanceLog
{
    public class CompleteMaintenanceDto
    {
        public required string PerformedByRef { get; set; }
        public string? WorkPerformed {  get; set; }
        public string? Notes {  get; set; }
    }
}
