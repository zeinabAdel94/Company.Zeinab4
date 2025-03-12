﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.Zeinab4.DAL.Modules;
using Microsoft.EntityFrameworkCore;

namespace Company.Zeinab4.DAL.Data.Context
{
    public  class CompanyDbContext:DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options):base( options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=CompanyZeinab4;Trusted_Connection=True; TrustServerCertification=True");
        //}

        public DbSet<Department>Departments{ get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
