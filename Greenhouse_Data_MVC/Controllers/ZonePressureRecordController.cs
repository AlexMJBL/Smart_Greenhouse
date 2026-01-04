using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;
using Greenhouse_Data_MVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Greenhouse_Data_MVC.Controllers
{
    public class ZonePressureRecordController : Controller
    {
        private readonly IZonePressureRecordServiceProxy _zonePressureRecordServiceProxy;
        private readonly IZoneServiceProxy _zoneServiceProxy;
        private readonly ILogger<ZonePressureRecordController> _logger;

        public ZonePressureRecordController(IZonePressureRecordServiceProxy zonePressureRecordServiceProxy,IZoneServiceProxy zoneServiceProxy ,ILogger<ZonePressureRecordController> logger)
        {
            _zonePressureRecordServiceProxy = zonePressureRecordServiceProxy;
            _zoneServiceProxy = zoneServiceProxy;
            _logger = logger;
        }

        // GET: ZonePressureRecordController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested zonePressureRecord list");
            var zonePressureRecords = await _zonePressureRecordServiceProxy.GetAllAsync();
            return View(zonePressureRecords);
        }

        // GET: ZonePressureRecordController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var zonePressureRecord = await _zonePressureRecordServiceProxy.GetByIdAsync(id);
            if (zonePressureRecord == null)
            {
                _logger.LogWarning("Unable to retrieve zonePressureRecord with id {zonePressureRecordId}", id);
                return NotFound();
            }

             var zone = await _zoneServiceProxy.GetByIdAsync(zonePressureRecord.ZoneId);
            if( zone == null)
             {
                _logger.LogWarning("Unable to retrieve zone attached to zonePressureRecord with id {zonePressureRecordId}", id);
                return NotFound();
            }

            var vm = new ZonePressureRecordViewModel
            {
                ZonePressureRecord = zonePressureRecord,
                Zone = zone
            };
            
            _logger.LogInformation("User requested zonePressureRecord with id : {zonePressureRecordId}", id);
            return View(vm);
        }

        // GET: ZonePressureRecordController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var zonePressureRecord = await _zonePressureRecordServiceProxy.GetByIdAsync(id);

            if (zonePressureRecord == null)
            {
                _logger.LogWarning("Unable to retrieve zonePressureRecord with id {zonePressureRecordId}",id);

                return NotFound();
            }

            return View(zonePressureRecord);
        }

        // POST: ZonePressureRecordController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _zonePressureRecordServiceProxy.DeleteAsync(id);
                _logger.LogInformation("zonePressureRecord with id {zonePressureRecordId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting zonePressureRecord with id {zonePressureRecordId}", id);
                return Problem("An error occurred while deleting the zonePressureRecord.");
            }
        }
    }
}
