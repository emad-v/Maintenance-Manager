using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Enums;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly MaintenanceManagerDbContext _context;

        public ComponentRepository(MaintenanceManagerDbContext context)
        {
            _context = context;
        }
        public async Task<Component> AddComponent(Component component)
        {
            component.CreatedDate = DateTime.UtcNow;
            ComponentDb componentDb = MapToComponentDb(component);
            _context.Components.Add(componentDb);
            await _context.SaveChangesAsync();

            return MapToComponent(componentDb);
        }

        public async Task<Component> GetComponentByReference(string reference)
        {
            ComponentDb? componentDb = await _context.Components.SingleOrDefaultAsync(x => x.Reference == reference);
            if (componentDb == null)
            {
                throw new KeyNotFoundException($"Component with reference: {reference} was not found");
            }
            return MapToComponent(componentDb);
        }

        public async Task<bool> ReferenceExistAsync(string reference)
        {
            return await _context.Components.AnyAsync(c=>c.Reference==reference);
        }

        private Component MapToComponent(ComponentDb componentDb)
        {
            return new Component
            {
                Reference = componentDb.Reference,
                MachineReference = componentDb.MachineReference,
                Name = componentDb.Name,
                Type = componentDb.Type,
                SerialNo = componentDb.SerialNo,
                CreatedDate = componentDb.CreatedDate
            };
        }

        private ComponentDb MapToComponentDb(Component component)
        {
            return new ComponentDb
            {
                Reference = component.Reference,
                MachineReference = component.MachineReference,
                Name = component.Name,
                Type = component.Type,
                SerialNo = component.SerialNo,
                CreatedDate = component.CreatedDate
            };
        }
       

       
    }
}



