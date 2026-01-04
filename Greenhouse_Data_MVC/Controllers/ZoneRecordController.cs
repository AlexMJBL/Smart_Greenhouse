using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;
using Greenhouse_Data_MVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Greenhouse_Data_MVC.Controllers
{
    public class ZoneRecordController : Controller
    {
        private readonly IZoneRecordServiceProxy _zoneRecordServiceProxy;
        private readonly IZoneServiceProxy _zoneServiceProxy;
        private readonly ILogger<ZoneRecordController> _logger;

        public ZoneRecordController(IZoneRecordServiceProxy zoneRecordServiceProxy,IZoneServiceProxy zoneServiceProxy ,ILogger<ZoneRecordController> logger)
        {
            _zoneRecordServiceProxy = zoneRecordServiceProxy;
            _zoneServiceProxy = zoneServiceProxy;
            _logger = logger;
        }

        // GET: ZoneRecordController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested zoneRecord list");
            var zoneRecords = await _zoneRecordServiceProxy.GetAllAsync();
            return View(zoneRecords);
        }

        // GET: ZoneRecordController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var zoneRecord = await _zoneRecordServiceProxy.GetByIdAsync(id);
            if (zoneRecord == null)
            {
                _logger.LogWarning("Unable to retrieve zoneRecord with id {zoneRecordId}", id);
                return NotFound();
            }

             var zone = await _zoneServiceProxy.GetByIdAsync(zoneRecord.ZoneId);
            if( zone == null)
             {
                _logger.LogWarning("Unable to retrieve zone attached to zoneRecord with id {zoneRecordId}", id);
                return NotFound();
            }

            var vm = new ZoneRecordViewModel
            {
                ZoneRecord = zoneRecord,
                Zone = zone
            };
            
            _logger.LogInformation("User requested zoneRecord with id : {zoneRecordId}", id);
            return View(vm);
        }

        // GET: ZoneRecordController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var zoneRecord = await _zoneRecordServiceProxy.GetByIdAsync(id);

            if (zoneRecord == null)
            {
                _logger.LogWarning("Unable to retrieve zoneRecord with id {zoneRecordId}",id);

                return NotFound();
            }

            return View(zoneRecord);
        }

        // POST: ZoneRecordController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _zoneRecordServiceProxy.DeleteAsync(id);
                _logger.LogInformation("zoneRecord with id {zoneRecordId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting zoneRecord with id {zoneRecordId}", id);
                return Problem("An error occurred while deleting the zoneRecord.");
            }
        }
    }
}
