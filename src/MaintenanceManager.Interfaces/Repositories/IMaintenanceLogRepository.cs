using MaintenanceManager.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Interfaces.Repositories
{
    public interface IMaintenanceLogRepository
    {
        Task<MaintenanceLog> AddLog(MaintenanceLog log);
        Task<IEnumerable<MaintenanceLog>> GetLogsByStatus(string statusReference);

    }
}
