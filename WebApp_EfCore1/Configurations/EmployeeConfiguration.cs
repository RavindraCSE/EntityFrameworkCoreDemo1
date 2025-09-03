using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp_EfCore1.Data;

namespace WebApp_EfCore1.Configurations
{
    // we use the Fluent API in place of data annotations 
    // its is much easier than that
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.EmployeeName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Gender).IsRequired().HasMaxLength(2);
            builder.Property(x => x.Age).IsRequired().HasDefaultValue(18);
        }
    }
}
