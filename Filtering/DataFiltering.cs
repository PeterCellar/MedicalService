using System.Reflection;
using MedicalService.Data.Models;
using MedicalService.Services;
using System.Globalization;

namespace MedicalService.Filtering;

class DataFiltering
{

    /// <summary>
    /// Filters covid vaccination data by filter parameters
    /// </summary>
    /// <param name="data">Generic collection of covid vaccination data</param>
    /// <param name="filter">Generic collection containing filter parameters</param>
    /// <param name="logger"></param>
    /// <returns>Collection of filtered covid vaccination data</returns>
    public static IEnumerable<T1>? FilterVaccinationData<T1, T2>(IEnumerable<T1>? records, T2 filter, ILogger<GreeterService> logger)
    where T2 : IFilter
    {
        IEnumerable<T1>? filteredRecords = records ;

        if (filteredRecords != null)
        {
            if (filter.DoubleFilter != null)
            {
                foreach (var filteringPair in filter.DoubleFilter)
                {
                    filteredRecords = filteredRecords.Where(record => 
                    {
                        PropertyInfo? propertyInfo = typeof(T1).GetProperty(filteringPair.Key);
                        
                        if(propertyInfo != null)
                        {
                            var propertyValue = propertyInfo.GetValue(record);
                            return propertyValue != null && double.Parse(propertyValue.ToString(), CultureInfo.InvariantCulture) == filteringPair.Value;
                        }
                        
                        return false;
                    });
                }
            }

            if (filter.StringFilter != null)
            {
                foreach (var filteringPair in filter.StringFilter)
                {
                    filteredRecords = filteredRecords.Where(record => 
                    {
                        PropertyInfo? propertyInfo = typeof(T1).GetProperty(filteringPair.Key);
                        
                        if(propertyInfo != null)
                        {
                            var propertyValue = propertyInfo.GetValue(record);
                            return propertyValue != null && propertyValue.ToString() == filteringPair.Value;
                        }
                        
                        return false;
                    });
                }
            }
        }
       
        return filteredRecords;
    }
}