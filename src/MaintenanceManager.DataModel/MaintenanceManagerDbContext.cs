using MaintenanceManager.Data.Models;
using MaintenanceManager.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data
{
    public class MaintenanceManagerDbContext : DbContext // translator that converts C# objects to SQL commands 


    {


        public MaintenanceManagerDbContext(
             DbContextOptions<MaintenanceManagerDbContext> options) : base(options)
        {
        }


        public DbSet<CustomerDb> Customers { get; set; } // this will becomes a table with add-migration
        public DbSet<MachineDb> Machines { get; set; }
        public DbSet<ComponentDb> Components { get; set; }
        public DbSet<MaintenanceRuleDb> MaintenanceRules { get; set; }
        public DbSet<ComponentRuleStatusDb> ComponentRuleStatuses { get; set; }
        public DbSet<UsageCounterDb> UsageCounters { get; set; }
        public DbSet<UserDb> Users { get; set; }
        public DbSet<UserCustomerDb> UserCustomers { get; set; }
        //public DbSet<NotificationDb> Notifications { get; set; }
        //public DbSet<MaintenanceLogDb> MaintenanceLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDb>()
                .HasIndex(c => c.Reference)
                .IsUnique();
            modelBuilder.Entity<MachineDb>()
                .HasIndex(m => m.Reference)
                .IsUnique();
            modelBuilder.Entity<ComponentDb>()
               .HasIndex(c => c.Reference)
               .IsUnique();

            modelBuilder.Entity<MaintenanceRuleDb>()
               .HasIndex(m => m.Reference)
               .IsUnique();
            modelBuilder.Entity<ComponentRuleStatusDb>()
               .HasIndex(c => c.Reference)
               .IsUnique();

            modelBuilder.Entity<ComponentRuleStatusDb>()
                .HasIndex(c => new { c.ComponentReference, c.MaintenanceRuleReference })
                .IsUnique();

            modelBuilder.Entity<UsageCounterDb>()
               .HasIndex(u => u.Reference)
               .IsUnique();
            modelBuilder.Entity<UsageCounterDb>()
               .HasIndex(u => new { u.ComponentReference, u.CounterType })
               .IsUnique();

            modelBuilder.Entity<UserDb>()
                .HasIndex(u => u.Reference)
                .IsUnique();

            modelBuilder.Entity<UserCustomerDb>()
                .HasIndex(u => u.Reference)
                .IsUnique();

            modelBuilder.Entity<UserCustomerDb>()
                .HasIndex(uc => new { uc.UserReference, uc.CustomerReference })
                .IsUnique();
            //modelBuilder.Entity<NotificationDb>()
            //    .HasIndex(u => u.Reference)
            //    .IsUnique();

            //modelBuilder.Entity<MaintenanceLogDb>()
            //    .HasIndex(m => m.Reference)
            //    .IsUnique();



        }



    }
}
