using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Repositories
{
    public class MaintenanceLogRepository : IMaintenanceLogRepository
    {
        private readonly MaintenanceManagerDbContext _context;

        public MaintenanceLogRepository(MaintenanceManagerDbContext context)
        {
            _context = context;
        }


        public async Task<MaintenanceLog> AddLog(MaintenanceLog maintenanceLog)
        {

            //maintenanceLog.CreatedAt = DateTime.UtcNow;
            //var maintenanceLogDb = MapToMaintenanceLogDb(maintenanceLog);
            //_context.MaintenanceLogs.Add(maintenanceLogDb);
            //await _context.SaveChangesAsync();
            //return MapToMaintenanceLog(maintenanceLogDb);
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<MaintenanceLog>> GetLogsByStatus(string statusReference)
        {
            //var maintenanceLogsDb = await _context.MaintenanceLogs
            //    .Where(m => m.StatusReference == statusReference)
            //    .ToListAsync();

            //return maintenanceLogsDb.Select(m => MapToMaintenanceLog(m));
            throw new NotImplementedException();
        }


        private static MaintenanceLogDb MapToMaintenanceLogDb(MaintenanceLog MaintenanceLog)
        {
            return new MaintenanceLogDb
            {
                Reference = MaintenanceLog.Reference,
                StatusReference = MaintenanceLog.StatusReference,
                PerformedBy = MaintenanceLog.PerformedBy,
                CompletedAt = MaintenanceLog.CompletedAt,
                WorkPerformed = MaintenanceLog.WorkPerformed,
                Notes = MaintenanceLog.Notes,
                CreatedAt = MaintenanceLog.CreatedAt,
            };
        }
        private static MaintenanceLog MapToMaintenanceLog(MaintenanceLogDb maintenanceLogDb)
        {
            return new MaintenanceLog
            {
                Reference = maintenanceLogDb.Reference,
                StatusReference = maintenanceLogDb.StatusReference,
                PerformedBy = maintenanceLogDb.PerformedBy,
                CompletedAt = maintenanceLogDb.CompletedAt,
                WorkPerformed = maintenanceLogDb.WorkPerformed,
                Notes = maintenanceLogDb.Notes,
                CreatedAt = maintenanceLogDb.CreatedAt,
            };
        }

    }
}
