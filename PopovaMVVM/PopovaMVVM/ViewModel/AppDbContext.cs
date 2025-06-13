using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PopovaMVVM.Model;

namespace PopovaMVVM.ViewModel
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AdditionalPayment> AdditionalPayments { get; set; }
        public DbSet<Staffing> Staffing { get; set; }
        public DbSet<Child> Children { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(LocalDB)\MSSQLLocalDB;Database=PopovaMVVM;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связей
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Children)
                .WithOne()
                .HasForeignKey(c => c.EmployeeId);
        }

    }
}
