using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Net;
using MedicalService.Services;
using MedicalService.Filtering;

namespace MedicalService.Data.Models;
class FileOperations
{
    /// <summary>
    /// Downloads covid vaccination data file. https://covid19.who.int/who-data/vaccination-data.csv
    /// </summary>
    public static async Task DownloadVaccinationData(ILogger<GreeterService> logger)
    {
        string vaccinationDataFile = "C:\\Users\\User\\OneDrive\\grpcService\\MedicalService\\Data\\vaccination-data.csv";
        string vaccinationMetadataFile = "C:\\Users\\User\\OneDrive\\grpcService\\MedicalService\\Data\\vaccination-metadata.csv";
        string vaccinationMetadataUri = "https://covid19.who.int/who-data/vaccination-metadata.csv";
        string vaccinationDataUri = "https://covid19.who.int/who-data/vaccination-data.csv";

        var dataResources = new List<Tuple<string, string>>()
        {
            new(vaccinationDataUri, vaccinationDataFile),
            new(vaccinationMetadataUri, vaccinationMetadataFile)
        };

        HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        using (HttpClient client = new(handler))
        {
            try
            {
                HttpResponseMessage response;

                foreach (var (fileUri, filePath) in dataResources)
                {
                    response = await client.GetAsync(fileUri);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = response.Content.ReadAsStringAsync().Result;

                        File.WriteAllText(filePath, content);
                        logger.LogInformation("Successfully updated vaccination data.");
                    }
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
    public static async Task<IEnumerable<VaccinesData>?> ReadCovidData(DataFilter filter, ILogger<GreeterService> logger)
    {
        using var reader = new StreamReader("C:\\Users\\User\\OneDrive\\grpcService\\MedicalService\\Data\\vaccination-data.csv");
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

        await DownloadVaccinationData(logger);

        try
        {
            var records = csv.GetRecords<VaccinesData>().ToList();
            var filteredRecords = DataFiltering.FilterVaccinationData(records, filter, logger);

            logger.LogInformation("Successfully read vaccination data.");

            return filteredRecords;
        }
        catch
        {
            logger.LogError("Reading vaccination data failed.");
            return null;
        }
    }
}