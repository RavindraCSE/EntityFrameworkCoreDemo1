
using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace WebApp_EfCore1.Data
{
    public class FusionStackContext : DbContext

    {
        public FusionStackContext(DbContextOptions<FusionStackContext> options):base(options) { }
       
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
