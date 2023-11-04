using MedicalService.Data.Models;
using MedicalService.Services;

namespace MedicalService.Filtering;
class DataFiltering
{

    /// <summary>
    /// Filters covid vaccination data by filter parameters
    /// </summary>
    /// <param name="data">Covid vaccination data</param>
    /// <param name="filter">Structure containing filter parameters</param>
    /// <param name="logger"></param>
    /// <returns>Filtered covid vaccination data</returns>
    public static IEnumerable<VaccinesData>? FilterVaccinationData(IEnumerable<VaccinesData>? records, DataFilter filter, ILogger<GreeterService> logger)
    {
        IEnumerable<VaccinesData>? filteredRecords = null;

        if (records != null)
        {
            if (!string.IsNullOrEmpty(filter.Country))
            {
                filteredRecords = records.Where(record => record.Country == filter.Country);
                logger.LogInformation(@"Filtered record: {0}", filteredRecords.First().Country);
            }

            if (!string.IsNullOrEmpty(filter.DataSource) && filteredRecords != null)
            {
                filteredRecords = filteredRecords.Where(record => record.DataSource == filter.DataSource);
                logger.LogInformation(@"Filtered record: {0}", filteredRecords.First().DataSource);
            }

            if (filteredRecords != null && filter.TotalVaccinations != 0 && filter.TotalVaccinationsPerHundred != 0)
            {
                filteredRecords = filteredRecords.Where(record => record.TotalVaccinations == filter.TotalVaccinations &&
                                                        record.TotalVaccinationsPerHundred == filter.TotalVaccinationsPerHundred);

                logger.LogInformation(@"Filtered records: {0}, {1}", filteredRecords.First().TotalVaccinations, filteredRecords.First().TotalVaccinationsPerHundred);
            }

        }

        return filteredRecords;
    }
}