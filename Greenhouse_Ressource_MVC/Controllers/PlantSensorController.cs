using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_Ressource_MVC.Controllers
{
    public class PlantSensorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
