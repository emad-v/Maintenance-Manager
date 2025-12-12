using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Repositories
{
    public class ComponentRuleStatusRepository : IComponentRuleStatusRepository
    {
        private readonly MaintenanceManagerDbContext _context;

        public ComponentRuleStatusRepository(MaintenanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<ComponentRuleStatus> AddStatus(ComponentRuleStatus componentRuleStatus)
        {
            componentRuleStatus.CreatedDate = DateTime.UtcNow;
            ComponentRuleStatusDb componentRuleStatusDb = MapToComponentRuleStatusDb(componentRuleStatus);
            _context.ComponentRuleStatuses.Add(componentRuleStatusDb);
            await _context.SaveChangesAsync();
            return MapToComponentRuleStatus(componentRuleStatusDb);
        }

        public async Task<ComponentRuleStatus> GetComponentRuleStatusByReference(string statusReference)
        {
            ComponentRuleStatusDb? componentRuleStatusDb = await _context.ComponentRuleStatuses.SingleOrDefaultAsync(c => c.Reference == statusReference);
            if (componentRuleStatusDb == null)
            {
                throw new KeyNotFoundException($"Status with reference: {statusReference} was not found");
            }
            return MapToComponentRuleStatus(componentRuleStatusDb);
        }


        public async Task<IEnumerable<ComponentRuleStatus>> GetAllStatusForComponent(string componentReference)
        {
            var statusesDb = await _context.ComponentRuleStatuses.Where(c => c.ComponentReference == componentReference).ToListAsync();
            return statusesDb.Select(c => MapToComponentRuleStatus(c));

        }

        public async Task<ComponentRuleStatus> UpdateMaintenanceStatus(string statusReference, DateTime lastServiceAt, int NextDueIn,int currentCounterValue)
        {
            ComponentRuleStatusDb? componentRuleStatusDb = await _context.ComponentRuleStatuses.SingleOrDefaultAsync(c => c.Reference == statusReference);
            if (componentRuleStatusDb == null)
            {
                throw new KeyNotFoundException($"Status with reference: {statusReference} was not found");
            }
           
            componentRuleStatusDb.LastServiceAt = lastServiceAt;
            componentRuleStatusDb.NextDueIn = NextDueIn;
            componentRuleStatusDb.IsOverDue = false;
            componentRuleStatusDb.LastMaintenanceCounterValue = currentCounterValue;
            await _context.SaveChangesAsync();
            return MapToComponentRuleStatus(componentRuleStatusDb);

        }


        public async Task DeleteStatus(string statusReference)
        {
            ComponentRuleStatusDb? componentRuleStatusDb = await _context.ComponentRuleStatuses.SingleOrDefaultAsync(c => c.Reference == statusReference);
            if (componentRuleStatusDb == null)
            {
                throw new KeyNotFoundException($"Status with reference: {statusReference} was not found");
            }
            _context.ComponentRuleStatuses.Remove(componentRuleStatusDb);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> ExistsAsync(string componentReference, string maintenanceRuleReference)
        {
            return await _context.ComponentRuleStatuses.AnyAsync(c => c.ComponentReference == componentReference && c.MaintenanceRuleReference == maintenanceRuleReference);

        }


        public async Task UpdateOverDue(string statusRef, int currentUsageValue)
        {
            ComponentRuleStatusDb? componentRuleStatusDb = await _context.ComponentRuleStatuses.SingleOrDefaultAsync(s => s.Reference == statusRef);
            if (componentRuleStatusDb == null)
            {
                throw new KeyNotFoundException($"Status with reference: {componentRuleStatusDb} was not found");
            }
            componentRuleStatusDb.IsOverDue = true;
            componentRuleStatusDb.LastMaintenanceCounterValue = currentUsageValue;
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateLastVisit(string statusRef,int updatedValue)
        //{
        //    ComponentRuleStatusDb? componentRuleStatusDb = await _context.ComponentRuleStatuses.SingleOrDefaultAsync(s => s.Reference == statusRef);
        //    if (componentRuleStatusDb == null)
        //    {
        //        throw new KeyNotFoundException($"Status with reference: {componentRuleStatusDb} was not found");
        //    }
        //    componentRuleStatusDb.LastVisitedThreshold = updatedValue;

        //}
     

        public async Task<IEnumerable<ComponentRuleStatus>> GetAllStatusForCounter(string counterRef)
        {
            var statusesDb = await _context.ComponentRuleStatuses.Where(c => c.UsageCounterReference == counterRef).ToListAsync();
            return statusesDb.Select(c => MapToComponentRuleStatus(c));

        }




        private static ComponentRuleStatusDb MapToComponentRuleStatusDb(ComponentRuleStatus componentRuleStatus)
        {
            return new ComponentRuleStatusDb()
            {
                Reference = componentRuleStatus.Reference,
                ComponentReference = componentRuleStatus.ComponentReference,
                MaintenanceRuleReference = componentRuleStatus.MaintenanceRuleReference,
                UsageCounterReference = componentRuleStatus.UsageCounterReference,
                LastServiceAt = componentRuleStatus.LastServiceAt,
                NextDueIn = componentRuleStatus.NextDueIn,
                IsOverDue = componentRuleStatus.IsOverDue,
                CreatedDate = componentRuleStatus.CreatedDate,
            };
        }
        private static ComponentRuleStatus MapToComponentRuleStatus(ComponentRuleStatusDb componentRuleStatusDb)
        {
            return new ComponentRuleStatus()
            {
                Reference = componentRuleStatusDb.Reference,
                ComponentReference = componentRuleStatusDb.ComponentReference,
                MaintenanceRuleReference = componentRuleStatusDb.MaintenanceRuleReference,
                UsageCounterReference = componentRuleStatusDb.UsageCounterReference,
                LastServiceAt = componentRuleStatusDb.LastServiceAt,
                NextDueIn = componentRuleStatusDb.NextDueIn,
                IsOverDue = componentRuleStatusDb.IsOverDue,
                CreatedDate = componentRuleStatusDb.CreatedDate,
            };
        }


        private async Task<ComponentRuleStatusDb> GetStatusDb(string statusReference)
        {
            ComponentRuleStatusDb? componentRuleStatusDb = await _context.ComponentRuleStatuses.SingleOrDefaultAsync(c => c.Reference == statusReference);
            if (componentRuleStatusDb == null)
            {
                throw new KeyNotFoundException($"Status with reference: {statusReference} was not found");
            }
            return componentRuleStatusDb;
        }
    }
}
