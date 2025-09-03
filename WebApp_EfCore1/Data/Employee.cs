namespace WebApp_EfCore1.Data
{
    public class Employee : AuditEntity
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Address {  get; set; }
    }
}
