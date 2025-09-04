using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApp_EfCore1.Data;
using WebApp_EfCore1.Data.StoredProcedures;
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
            string department = "IT";
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

            //var result = (from emp in dbContext.Employees
            //             join dep in dbContext.Department
            //             on emp.Id equals dep.Id
            //             where emp.Id== id
            //             select new EmployeeDetailsViewModel // converted in the viewmodel as per target
            //             {
            //                EmployeeName= emp.EmployeeName,
            //                Age= emp.Age,
            //                Gender= emp.Gender,
            //                Address= emp.Address,
            //                Department = dep.DepartmentName
            //                }).ToList();

            // using the method syntax
            var result = dbContext.Employees
                        .Join(dbContext.Department, 
                        emp => emp.DepartmentId,
                        dep => dep.Id,
                        (emp, dep) => new { emp, dep })
                        .Where(x=>x.emp.Id==id)
                        .Select(x=>new EmployeeDetailsViewModel {
                        EmployeeName= x.emp.EmployeeName,
                        Age= x.emp.Age,
                        Gender= x.emp.Gender,
                        Address= x.emp.Address,
                        Department = x.dep.DepartmentName
                        
                        });
           
            return View(result);
        }

        [Route("GetEmpDetailsByNavigation/{id:int}")]
        public IActionResult GetEmpDetailsByNavigation(int id)
        {

            var result = dbContext.Employees
                         .Include(x => x.Department)
                         .ThenInclude(d=>d.City)
                         .Where(x => x.Id == id)
                         .Select(x=>new GetEmpDetailsByNavigationViewModel
                         {
                             EmployeeName = x.EmployeeName,
                             Age = x.Age,
                             Gender = x.Gender,
                             Address = x.Address,
                             Department = x.Department.DepartmentName,
                             City=x.Department.City.LocationName

                         }).ToList();



            return View(result);
        }

        [Route("GetEmpDetailsByStoredProcedure/{id:int}")]
        public IActionResult GetEmpDetailsByStoredProcedure(int id)
        {

            var result = dbContext.Set<FindEmployeesDTO>().FromSqlInterpolated($"Exec dbo.GetEmpById @Id={id}").ToList();


            return View(result);
        }

        [Route("GetEmpDetailsByStoredProcedureParam/{id:int}")]
        public IActionResult GetEmpDetailsByStoredProcedureParam(int id)
        {
            var totalCountParam = new SqlParameter 
            { 
            ParameterName = "@TotalCount",
            SqlDbType= System.Data.SqlDbType.Int,
            Direction= System.Data.ParameterDirection.Output
            };

            var userIdParam = new SqlParameter("@Id", id);
            var result = dbContext.Set<FindEmployeesDTO>()
                        .FromSqlRaw("Exec dbo.GetEmpById_with_OutputParam @Id,@TotalCount output", userIdParam, totalCountParam)
                        .ToList();
                
                


            return View(result);
        }
    }
}
