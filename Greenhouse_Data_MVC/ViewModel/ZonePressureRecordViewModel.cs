
using Greenhouse_Data_MVC.Dtos;

namespace Greenhouse_Data_MVC.ViewModel
{
    public class ZonePressureRecordViewModel
    {
        public ZonePressureRecordDto ZonePressureRecord { get; set; }
        public ZoneDto Zone { get; set;}

        public SensorDto Sensor {get;set;}
    }
}