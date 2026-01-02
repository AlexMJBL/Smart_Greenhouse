using Greenhouse_Data_MVC.ViewModel;
using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;
using Greenhouse_Data_MVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Greenhouse_Data_MVC.Controllers
{
    public class WateringController : Controller
    {
        private readonly IWateringServiceProxy _wateringServiceProxy;
        private readonly IPlantServiceProxy _plantServiceProxy;
        private readonly ILogger<WateringController> _logger;

        public WateringController(IWateringServiceProxy wateringServiceProxy,IPlantServiceProxy plantServiceProxy ,ILogger<WateringController> logger)
        {
            _wateringServiceProxy = wateringServiceProxy;
            _plantServiceProxy = plantServiceProxy;
            _logger = logger;
        }

        // GET: WateringController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested watering list");
            var waterings = await _wateringServiceProxy.GetAllAsync();
            return View(waterings);
        }

        // GET: WateringController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var watering = await _wateringServiceProxy.GetByIdAsync(id);
            if (watering == null)
            {
                _logger.LogWarning("Unable to retrieve watering with id {wateringId}", id);
                return NotFound();
            }


            var plant = await _plantServiceProxy.GetByIdAsync(watering.PlantId);
            if (plant == null)
            {
                _logger.LogWarning("Unable to retrieve plant attached to watering with id {wateringId}", id);
                return NotFound();
            }

            var viewModel = new WateringViewModel
            {
                Watering= watering,
                Plant = plant
            };

            _logger.LogInformation("User requested watering with id : {wateringId}", id);
            return View(viewModel);
        } 
        private async Task LoadPlantsAsync()
        {
            var plants = await _plantServiceProxy.GetAllAsync();
            ViewBag.Plants = plants.Select(c =>
               new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Description
               });
        }
    }
}
