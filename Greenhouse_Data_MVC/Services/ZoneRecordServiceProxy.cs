using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class ZoneRecordServiceProxy : ServiceProxy<ZoneRecordDto> , IZoneRecordServiceProxy
    {
        public ZoneRecordServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base (httpClientFactory, config, "/zoneRecords")
        { 
        }
    }
}
