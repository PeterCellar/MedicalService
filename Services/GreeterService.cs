using Grpc.Core;
using MedicalService.Data.Models;
using MedicalService.Helper;

namespace MedicalService.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }


    /// <summary>
    /// Sends gRPC TotalVaccinesReply to a client 
    /// </summary>
    /// <param name="request">gRPC request</param>
    /// <param name="replyStream">gRPC reply stream</param>
    /// <param name="context">ServerContext</param>
    /// <returns>Stream with TotalVaccinesReplies</returns>
    public override async Task GetTotalVaccinesByState(TotalVaccinesRequest request, IServerStreamWriter<TotalVaccinesReply> replyStream, ServerCallContext context)
    {
        _logger.LogInformation("Request for vaccination data recieved.");

        await FileOperations.DownloadVaccinationData(_logger);

        IEnumerable<VaccinesData>? covidData = null;
        if (request.Filter != null)
        {
            covidData = FileOperations.ReadFilteredCovidData(request.Filter, _logger);
        }

        if (covidData != null)
        {
            int counter = 1;
            foreach (var data in covidData)
            {
                var reply = DataHelper.FillTotalVaccinesReply(data);
                
                await replyStream.WriteAsync(reply);
                _logger.LogInformation(@"Sent reply [{0}] to the client.", counter);

                await Task.Delay(TimeSpan.FromSeconds(1));
                counter++;
            }
        }
    }
}
