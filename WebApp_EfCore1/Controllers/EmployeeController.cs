using Microsoft.AspNetCore.Mvc;
using WebApp_EfCore1.Data;
using WebApp_EfCore1.Models;

namespace WebApp_EfCore1.Controllers
{
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        public FusionStackContext dbContext { get; }
        public EmployeeController(FusionStackContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            // Insert record in employee
            string department = "Inventory";
            // get the department detail first
            var dep = dbContext.Department.Where(x => x.DepartmentName == department).FirstOrDefault();
            if (dep is not null)
            {

                var emp = new Employee()
                {
                    EmployeeName = "Jayas",
                    Gender = "M",
                    Age = 30,
                    Address = "Barabanki",
                    DepartmentId = dep.Id,
                };

                dbContext.Employees.Add(emp);
                dbContext.SaveChanges();

            }
            return View();
        }

        [Route("update/{id:int}/{updatedName}")]
        public IActionResult UpdateEmployee(int id, string updatedName)
        {
            var employeeDetails = dbContext.Employees.FirstOrDefault(x => x.Id == id);
            if(employeeDetails is not null)
            {
                employeeDetails.EmployeeName = updatedName;
                dbContext.SaveChanges();
            }
            return View(employeeDetails);
        }

        [Route("GetEmpDetails/{id:int}")]
        public IActionResult GetEmpDetails(int id)
        {

            //var result1 = (from emp in dbContext.Employees
            //              join dep in dbContext.Department
            //              on emp.Id equals dep.Id
            //              where emp.Id == id
            //              select new Employee
            //              {
            //                  EmployeeName = emp.EmployeeName,
            //                  Age = emp.Age,
            //                  Gender = emp.Gender,
            //                  Address = emp.Address,
            //                 // Department = dep.DepartmentName
            //              }).AsEnumerable();

           
            // create join with employee and department table to get the record
            var result2 = (from emp in dbContext.Employees
                         join dep in dbContext.Department
                         on emp.Id equals dep.Id
                         where emp.Id== id
                         select new EmployeeDetailsViewModel // converted in the viewmodel as per target
                         {
                            EmployeeName= emp.EmployeeName,
                            Age= emp.Age,
                            Gender= emp.Gender,
                            Address= emp.Address,
                            Department = dep.DepartmentName
                            }).ToList();
           
            return View(result2);
        }
    }
}
