using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Greenhouse_Ressource_MVC.Controllers
{
    public class specimenController : Controller
    {
        private readonly ISpecimenServiceProxy _specimenServiceProxy;
        private readonly ISoilHumidityCategoryServiceProxy _soilHumidityCategoryServiceProxy;
        private readonly ILogger<specimenController> _logger;

        public specimenController(ISpecimenServiceProxy specimenServiceProxy,ISoilHumidityCategoryServiceProxy soilHumidityCategoryServiceProxy ,ILogger<specimenController> logger)
        {
            _specimenServiceProxy = specimenServiceProxy;
            _soilHumidityCategoryServiceProxy = soilHumidityCategoryServiceProxy;
            _logger = logger;
        }

        // GET: SpecimenController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested specimen list");
            var specimens = await _specimenServiceProxy.GetAllAsync();
            return View(specimens);
        }

        // GET: SpecimenController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var specimen = await _specimenServiceProxy.GetByIdAsync(id);
            if (specimen == null)
            {
                _logger.LogWarning("Unable to retrieve specimen with id {specimenId}", id);
                return NotFound();
            }
            _logger.LogInformation("User requested specimen with id : {specimenId}", id);
            return View(specimen);
        }

        // GET: SpecimenController/Create
        public async Task<IActionResult> Create()
        {
            await LoadSoilHumidityCategorysAsync();
            return View();
        }

        // POST: SpecimenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SpecimenWriteDto dto)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid specimen creation attempt");
                await LoadSoilHumidityCategorysAsync();
                return View(dto);
            }

            try
            {
               await _specimenServiceProxy.CreateAsync(dto);
                _logger.LogInformation("Specimen created successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while creating specimen");

                ModelState.AddModelError(string.Empty,"An error occurred while creating the specimen.");

                await LoadSoilHumidityCategorysAsync();
                return View(dto);
            }
        }

        // GET: SpecimenController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var specimen = await _specimenServiceProxy.GetByIdAsync(id);
            
            if (specimen == null)
            {
                _logger.LogWarning("Unable to retrieve specimen with id {specimenId}", id);
                return NotFound();
            }

            var writeDto = new SpecimenWriteDto
            {
                Name = specimen.Name,
                SoilHumidityCatId = specimen.SoilHumidityCatId,
            };

            await LoadSoilHumidityCategorysAsync();
            return View(writeDto);
        }

        // POST: SpecimenController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecimenWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid specimen modification attempt");
                await LoadSoilHumidityCategorysAsync();
                return View(dto);
            }
            try
            {
                await _specimenServiceProxy.UpdateAsync(id,dto);
                _logger.LogInformation("Specimen updated successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while updating specimen with id {specimenId}", id);

                ModelState.AddModelError(string.Empty, "An error occurred while updating the specimen.");

                await LoadSoilHumidityCategorysAsync();
                return View(dto);
            }
        }

        // GET: SpecimenController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var specimen = await _specimenServiceProxy.GetByIdAsync(id);

            if (specimen == null)
            {
                _logger.LogWarning("Unable to retrieve specimen with id {specimenId}",id);

                return NotFound();
            }

            return View(specimen);
        }

        // POST: SpecimenController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _specimenServiceProxy.DeleteAsync(id);
                _logger.LogInformation("Specimen with id {specimenId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting specimen with id {specimenId}", id);
                return Problem("An error occurred while deleting the specimen.");
            }

        }

        private async Task LoadSoilHumidityCategorysAsync()
        {
            var categories = await _soilHumidityCategoryServiceProxy.GetAllAsync();

            ViewBag.SoilHumidityCategories = categories.Select(c =>
                new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
        }
    }
}
