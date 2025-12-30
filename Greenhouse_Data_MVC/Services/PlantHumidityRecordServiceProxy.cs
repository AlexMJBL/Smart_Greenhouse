using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class PlantHumidityRecordServiceProxy : ServiceProxy<PlantHumidityRecordDto> , IPlantHumidityRecordServiceProxy
    {
        public PlantHumidityRecordServiceProxy(IHttpClientFactory httpClientFactory, IConfiguration config)
        : base (httpClientFactory, config, "plantHumidityRecords")
        { 
        }
    }
}
