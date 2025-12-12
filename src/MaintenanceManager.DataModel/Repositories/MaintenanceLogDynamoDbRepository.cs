using Amazon.DynamoDBv2.DataModel;
using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Repositories
{
    public class MaintenanceLogDynamoDbRepository : IMaintenanceLogRepository
    {
        private readonly DynamoDBContext _context;

        public MaintenanceLogDynamoDbRepository(DynamoDBContext context)
        {
            _context = context;
        }

        public async Task<MaintenanceLog> AddLog(MaintenanceLog log)
        {
            log.CreatedAt = DateTime.UtcNow;
            var maintenanceLogDynamoDb = MapToMaintenanceLogDynamoDb(log);
            await _context.SaveAsync(maintenanceLogDynamoDb);

            return MaptToMaintenanceLog(maintenanceLogDynamoDb);
        }

        public async Task<IEnumerable<MaintenanceLog>> GetLogsByStatus(string statusReference)
        {
            var statusPk = $"STATUS#{statusReference}";

            var search =  _context.QueryAsync<MaintenanceLogDynamoDb >(statusPk);

            var results = await search.GetRemainingAsync();

            return results.Select(MaptToMaintenanceLog);
        }


        private static MaintenanceLogDynamoDb MapToMaintenanceLogDynamoDb(MaintenanceLog maintenanceLog)
        {
            return new MaintenanceLogDynamoDb
            {
                PK = $"STATUS#{maintenanceLog.StatusReference}",
                SK = $"LOG#{maintenanceLog.CompletedAt:yyyyMMddHHmmss}",
                Reference = maintenanceLog.Reference,
                StatusReference = maintenanceLog.StatusReference,
                PerformedBy = maintenanceLog.PerformedBy,
                WorkPerformed = maintenanceLog.WorkPerformed,
                CompletedAt = maintenanceLog.CompletedAt,
                Notes = maintenanceLog.Notes,
                CreatedAt = maintenanceLog.CreatedAt,

            };
        }

        private static MaintenanceLog MaptToMaintenanceLog(MaintenanceLogDynamoDb maintenanceLogDynamoDb)
        {
            return new MaintenanceLog
            {
                Reference = maintenanceLogDynamoDb.Reference,
                StatusReference = maintenanceLogDynamoDb.StatusReference,
                PerformedBy = maintenanceLogDynamoDb.PerformedBy,
                CompletedAt = maintenanceLogDynamoDb.CompletedAt,
                Notes = maintenanceLogDynamoDb.Notes,
                CreatedAt = maintenanceLogDynamoDb.CreatedAt,
            };
        }
    }
}


