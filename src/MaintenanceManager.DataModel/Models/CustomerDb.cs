using MaintenanceManager.DomainModel.Entities;
using MaintenanceManager.DomainModel.Models.Customers;
using MaintenanceManager.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.Data.Models
{
    public class CustomerDb
    {

        public int Id { get; set; }
        public required string Reference {get; set;}
        public required string Name { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }






}
