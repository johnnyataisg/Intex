using Intex.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Intex.DAL
{
    public class IntexContext : DbContext
    {
        public IntexContext() : base("IntexContext")
        {

        }

        public DbSet<WorkOrders> workorders { get; set; }
        public DbSet<Compounds> compounds { get; set; }
        public DbSet<Customers> customers { get; set; }
        public DbSet<Samples> samples { get; set; }
        public DbSet<Employees> employees { get; set; }
        public DbSet<EmployeeUsers> employeeusers { get; set; }
        /*
        public DbSet<Assays> assays { get; set; }
        public DbSet<CustomerUsers> customerusers { get; set; }
        public DbSet<Invoices> invoices { get; set; }
        public DbSet<Tests> tests { get; set; }
        public DbSet<TestTubes> testtubes { get; set; }*/
    }
}