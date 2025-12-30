using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class ZonePressureRecordServiceProxy : ServiceProxy<ZonePressureRecordDto> , IZonePressureRecordServiceProxy
    {
        public ZonePressureRecordServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base (httpClientFactory, config, "/zonePressureRecords")
        { 
        }
    }
}
