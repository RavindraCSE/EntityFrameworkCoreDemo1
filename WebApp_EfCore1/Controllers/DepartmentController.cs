using Microsoft.AspNetCore.Mvc;
using WebApp_EfCore1.Data;

namespace WebApp_EfCore1.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly FusionStackContext dbContext;

        public DepartmentController(FusionStackContext dbContext)
        {
            this.dbContext=dbContext;
        }
        public IActionResult Index()
        {
            Department department = new Department() 
            { 
                DepartmentName="IT",
                //CreatedBy="system",
                //CreatedOn=DateTime.Now
                // no need to write we have overridden the savechanges() method in the 
                // dbContext configurationfile
            };
            dbContext.Department.Add(department);
            dbContext.SaveChanges();
            return View();
        }
    }
}
