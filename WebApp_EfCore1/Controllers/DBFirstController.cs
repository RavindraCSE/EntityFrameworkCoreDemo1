using Microsoft.AspNetCore.Mvc;
using WebApp_EfCore1.DBFirstModels;

namespace WebApp_EfCore1.Controllers
{
    public class DBFirstController : Controller
    {
        private readonly FusionStackDbFirstContext dbContext;

        public DBFirstController(FusionStackDbFirstContext dbContext)
        {
            this.dbContext= dbContext;
        }
        public IActionResult LoadEmployees(int id)
        {
            var employeeDetails = dbContext.Employees.FirstOrDefault(x => x.Id == id);
            return View();
        }
    }
}
