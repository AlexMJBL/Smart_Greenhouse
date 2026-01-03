
using Greenhouse_Data_MVC.Dtos;

namespace Greenhouse_Data_MVC.ViewModel
{
    public class ZoneSensorAlertViewModel
    {
        public ZoneSensorAlertDto ZoneSensorAlert { get; set; }
        public SensorDto Sensor { get; set; }
        public ZoneDto Zone { get; set; }
    }
}