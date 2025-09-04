namespace WebApp_EfCore1.Data.StoredProcedures
{
    public class FindEmployeesDTO
    {
       // public string Id { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
        public string LocationName { get; set; }
    }
}
