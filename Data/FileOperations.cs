using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.Net;
using MedicalService.Services;

namespace MedicalService.Data.Models;
class FileOperations
{
    /// <summary>
    /// Downloads covid vaccination data file. https://covid19.who.int/who-data/vaccination-data.csv
    /// </summary>
    public static async Task DownloadVaccinationData(ILogger<GreeterService> logger)
    {
        string filePath = "C:\\Users\\User\\OneDrive\\grpcService\\MedicalService\\Data\\vaccination-data.csv";
        string fileUri = "https://covid19.who.int/who-data/vaccination-data.csv";

        HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        using (HttpClient client = new(handler))
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(fileUri);

                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;

                    File.WriteAllText(filePath, content);
                    logger.LogInformation("Successfully updated vaccination data.");
                }
            }
            catch
            {
                logger.LogError("Updating vaccination data failed.");
            }
        }
    }

    /// <summary>
    /// Reads covid vaccination data from csv file. https://covid19.who.int/who-data/vaccination-data.csv
    /// </summary>
    /// <param name="CountryIso3">Filtering parameter</param>
    /// <returns>Filtered covid vaccination records</returns>
    public static IEnumerable<VaccinesData>? ReadFilteredCovidData(Filter filter, ILogger<GreeterService> logger)
    {
        using var reader = new StreamReader("C:\\Users\\User\\OneDrive\\grpcService\\MedicalService\\Data\\vaccination-data.csv");
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        IEnumerable<VaccinesData>? filteredRecords = null;

        try
        {
            var records = csv.GetRecords<VaccinesData>().ToList();
            filteredRecords = records.Where(record => record.Iso3 == filter.FilterOne);
            logger.LogInformation("Successfully read vaccination data.");

            return filteredRecords;
        }
        catch
        {
            logger.LogError("Reading vaccination data failed.");
            return filteredRecords;
        }
    }
}