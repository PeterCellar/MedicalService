using MedicalService.Data.Models;
using MedicalService.Services;

namespace MedicalService.Filtering;
class DataFiltering
{

    /// <summary>
    /// Filters covid vaccination data with country filter
    /// </summary>
    /// <param name="data">Covid vaccination data</param>
    /// <param name="country">Country filter for vaccination data</param>
    /// <param name="logger"></param>
    /// <returns>Filtered covid vaccination data</returns>
    public static IEnumerable<VaccinesData>? FilterVaccinationDataByCountry(IEnumerable<VaccinesData>? data, string country, ILogger<GreeterService> logger)
    {
        if(data != null && !string.IsNullOrEmpty(country))
        {
            var filteredData = data.Where(record => record.Country == country); 
            logger.LogInformation
            ($"Filtered record: {0}", filteredData.First().Country);
            return filteredData;
        }


        return data;
    }
}