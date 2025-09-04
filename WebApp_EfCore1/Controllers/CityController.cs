using Microsoft.AspNetCore.Mvc;
using WebApp_EfCore1.Data;

namespace WebApp_EfCore1.Controllers
{
    [Route("city")]
    public class CityController : Controller
    {
        private readonly FusionStackContext dbContext;

        public CityController(FusionStackContext dbContext)
        {
            this.dbContext= dbContext;
        }
        [Route("addcity/{cityname}")]
        public IActionResult Index(string cityname)
        {
            dbContext.Cities.Add(new City() { 
            LocationName= cityname
            });
            dbContext.SaveChanges();
            return View();
        }
    }
}
