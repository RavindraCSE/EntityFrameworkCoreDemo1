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
            var locationData = dbContext.Cities.FirstOrDefault(x => x.Id == 1);
            Department department = new Department() 
            { 
                DepartmentName="IT",
                //CreatedBy="system",
                //CreatedOn=DateTime.Now
                // no need to write we have overridden the savechanges() method in the 
                // dbContext configurationfile
                City= locationData
            };
            dbContext.Department.Add(department);
            dbContext.SaveChanges();
            return View();
        }
    }
}
