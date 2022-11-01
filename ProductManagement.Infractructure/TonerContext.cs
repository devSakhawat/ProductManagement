using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL
{
   public class TonerContext:DbContext
   {
      public TonerContext(DbContextOptions<TonerContext> options) : base(options)
      {

      }

      public DbSet<Customer> Customers { get; set; }
      public DbSet<Project> Projects { get; set; }
      public DbSet<Machine> Machines { get; set; }
      public DbSet<Toner> Toners { get; set; }
      public DbSet<DeliveryToner> DeliveryToners { get; set; }
      public DbSet<TonerUsage> TonerUsages { get; set; }
      public DbSet<PaperUsage> PaperUsages { get; set; }
      public DbSet<ProfitCalculation> ProfitCalculations { get; set; }
   }
}
