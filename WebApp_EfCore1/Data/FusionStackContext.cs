
using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace WebApp_EfCore1.Data
{
    public class FusionStackContext : DbContext

    {
        public FusionStackContext(DbContextOptions<FusionStackContext> options):base(options) { }
       
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Department { get; set; }

        public override int SaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries<AuditEntity>())
            {
                if(entry.State== EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTime.Now;
                    entry.Entity.CreatedBy = "systems";
                }
                if(entry.State== EntityState.Modified)
                {
                    entry.Entity.LastModifiedOn= DateTime.Now;
                    entry.Entity.LastModifiedBy = "systems";
                }
            }
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
