namespace WebApp_EfCore1.Data
{
    public class Department : AuditEntity
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<Employee> Employees { get; set; } // navigation property
        // we do not need to register in the dbContext File because it
        // //will autometically detect using the navigatio property
    }
}
