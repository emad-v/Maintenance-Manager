using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Enums;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Repositories
{
    public class UsageCounterRepository : IUsageCounterRepository
    {
        private readonly MaintenanceManagerDbContext _context;

        public UsageCounterRepository(MaintenanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<UsageCounter> AddCounter(UsageCounter counter)
        {
            counter.CreatedDate = DateTime.UtcNow;
            var counterDb = MapToUsageCounterDb(counter);
            _context.UsageCounters.Add(counterDb);
            await _context.SaveChangesAsync();
            return MapToUsageCounter(counterDb);
        }


        public async Task<UsageCounter> IncrementCounter(string counterReference, int incrementAmount)
        {
            var counterDb = await _context.UsageCounters.FirstOrDefaultAsync(c=>c.Reference == counterReference);
            if (counterDb == null)
            {
                throw new KeyNotFoundException($"Counter with reference: {counterReference} was not found");
            }
            counterDb.Value += incrementAmount;
            counterDb.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return MapToUsageCounter(counterDb);
        }


        public async Task<UsageCounter?> GetCounterTypeForComponent(string componentReference,CounterType type)
        {
            
            UsageCounterDb? uc = await _context.UsageCounters.SingleOrDefaultAsync(c => c.ComponentReference == componentReference && c.CounterType == type);
            if (uc == null)
            {
                return null;
            }
            return MapToUsageCounter(uc);
        }

        public async Task<bool> ReferenceExistAsync(string reference)
        {
            return await _context.UsageCounters.AnyAsync(c => c.Reference == reference);   
        }

        public async Task<UsageCounter> GetCounterByReference(string reference)
        {
            UsageCounterDb? counterDb = await _context.UsageCounters.SingleOrDefaultAsync(c=>c.Reference==reference);
            if (counterDb == null)
            {
                throw new KeyNotFoundException($"Counter with reference: {reference} was not found");
            }
            return MapToUsageCounter(counterDb);
        }


        private static UsageCounterDb MapToUsageCounterDb(UsageCounter counter)
        {
            return new UsageCounterDb()
            {
                Reference = counter.Reference,
                ComponentReference = counter.ComponentReference,
                CounterType = counter.CounterType,
                Value = counter.Value,
                UpdatedAt = counter.UpdatedAt,
                CreatedDate = counter.CreatedDate,
            };

        }
        private static UsageCounter MapToUsageCounter(UsageCounterDb usageCounterDb)
        {
            return new UsageCounter()
            {
                Reference = usageCounterDb.Reference,
                ComponentReference = usageCounterDb.ComponentReference,
                CounterType = usageCounterDb.CounterType,
                Value = usageCounterDb.Value,
                UpdatedAt = usageCounterDb.UpdatedAt,
                CreatedDate = usageCounterDb.CreatedDate
            };
        }
       
    }
}
