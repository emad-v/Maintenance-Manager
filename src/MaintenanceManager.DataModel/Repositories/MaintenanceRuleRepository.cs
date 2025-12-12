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
    public class MaintenanceRuleRepository : IMaintenanceRuleRepository
    {
        private readonly MaintenanceManagerDbContext _context;

        public MaintenanceRuleRepository(MaintenanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<MaintenanceRule> AddMaintenanceRule(MaintenanceRule maintenanceRule)
        {
            maintenanceRule.CreatedDate = DateTime.UtcNow;
            var maintenanceRuleDb = MapToMaintenanceRuleDb(maintenanceRule);
            _context.MaintenanceRules.Add(maintenanceRuleDb);
            await _context.SaveChangesAsync();
            return MapToMaintenanceRule(maintenanceRuleDb);
        }

        public async Task<MaintenanceRule> GetMaintenanceRuleByReference(string reference)
        {

            var maintenanceRuleDB = await _context.MaintenanceRules.SingleOrDefaultAsync(m => m.Reference == reference);
            if (maintenanceRuleDB == null)
            {
                throw new KeyNotFoundException($"Maintenance Rule with reference {reference} was not found");
            }
            return MapToMaintenanceRule(maintenanceRuleDB);

        }

        public async Task<IEnumerable<MaintenanceRule>> GetMaintenanceRules()
        {
            var maintenanceRulesDb = await _context.MaintenanceRules.ToListAsync();

            return maintenanceRulesDb.Select(maintenanceRulesDb => MapToMaintenanceRule(maintenanceRulesDb));
        }


        public async Task<bool> ReferenceExistAsync(string reference)
        {
            return await _context.MaintenanceRules.AnyAsync(m => m.Reference == reference);
        }

        private static MaintenanceRuleDb MapToMaintenanceRuleDb(MaintenanceRule maintenanceRule)
        {
            return new MaintenanceRuleDb()
            {
                Reference = maintenanceRule.Reference,
                RuleName = maintenanceRule.RuleName,
                CounterType = maintenanceRule.CounterType,
                IntervalValue = maintenanceRule.IntervalValue,
                Description = maintenanceRule.Description,
                AppliesTo = maintenanceRule.AppliesTo,
                CreatedDate = maintenanceRule.CreatedDate
            };
        }

        private static MaintenanceRule MapToMaintenanceRule(MaintenanceRuleDb maintenanceRuleDb)
        {
            return new MaintenanceRule()
            {
                Reference = maintenanceRuleDb.Reference,
                RuleName = maintenanceRuleDb.RuleName,
                CounterType = maintenanceRuleDb.CounterType,
                IntervalValue = maintenanceRuleDb.IntervalValue,
                Description = maintenanceRuleDb.Description,
                AppliesTo = maintenanceRuleDb.AppliesTo,
                CreatedDate = maintenanceRuleDb.CreatedDate

            };
        }

      
    }
}