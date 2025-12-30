using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;
using Greenhouse_Data_MVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Greenhouse_Data_MVC.Controllers
{
    public class ZoneSensorAlertController : Controller
    {
        private readonly IZoneSensorAlertServiceProxy _zoneSensorAlertServiceProxy;
        private readonly ISensorServiceProxy _sensorServiceProxy;
        private readonly ILogger<ZoneSensorAlertController> _logger;

        public ZoneSensorAlertController(IZoneSensorAlertServiceProxy zoneSensorAlertServiceProxy,ISensorServiceProxy sensorServiceProxy ,ILogger<ZoneSensorAlertController> logger)
        {
            _zoneSensorAlertServiceProxy = zoneSensorAlertServiceProxy;
            _sensorServiceProxy = sensorServiceProxy;
            _logger = logger;
        }

        // GET: ZoneSensorAlertController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested zoneSensorAlert list");
            var zoneSensorAlerts = await _zoneSensorAlertServiceProxy.GetAllAsync();
            return View(zoneSensorAlerts);
        }

        // GET: ZoneSensorAlertController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var zoneSensorAlert = await _zoneSensorAlertServiceProxy.GetByIdAsync(id);
            if (zoneSensorAlert == null)
            {
                _logger.LogWarning("Unable to retrieve zoneSensorAlert with id {zoneSensorAlertId}", id);
                return NotFound();
            }

             var sensor = await _sensorServiceProxy.GetByIdAsync(zoneSensorAlert.SensorId);
            if( sensor == null)
             {
                _logger.LogWarning("Unable to retrieve zone with id {SensorId}", zoneSensorAlert.SensorId);
                return NotFound();
            }

            var vm = new ZoneSensorAlertViewModel
            {
                ZoneSensorAlert = zoneSensorAlert,
                Sensor = sensor
            };
            
            _logger.LogInformation("User requested zoneSensorAlert with id : {zoneSensorAlertId}", id);
            return View(zoneSensorAlert);
        }

        // GET: ZoneSensorAlertController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var zoneSensorAlert = await _zoneSensorAlertServiceProxy.GetByIdAsync(id);

            if (zoneSensorAlert == null)
            {
                _logger.LogWarning("Unable to retrieve zoneSensorAlert with id {zoneSensorAlertId}",id);

                return NotFound();
            }

            return View(zoneSensorAlert);
        }

        // POST: ZoneSensorAlertController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _zoneSensorAlertServiceProxy.DeleteAsync(id);
                _logger.LogInformation("zoneSensorAlert with id {zoneSensorAlertId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting zoneSensorAlert with id {zoneSensorAlertId}", id);
                return Problem("An error occurred while deleting the zoneSensorAlert.");
            }
        }
    }
}
