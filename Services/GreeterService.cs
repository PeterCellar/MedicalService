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
    /// Sends vaccines data to a client
    /// </summary>
    /// <param name="request">gRPC request</param>
    /// <param name="replyStream">gRPC reply stream</param>
    /// <param name="context">ServerCallContext</param>
    /// <returns>Stream of the VaccineDataReplies</returns>
    public override async Task GetVaccinesData(VaccinesDataRequest request, IServerStreamWriter<VaccinesDataReply> replyStream, ServerCallContext context)
    {
        _logger.LogInformation("Request for vaccination data recieved.");

        var covidData = FileOperations.ReadCovidData(request.Filter, _logger);
        
        if (covidData.Result != null)
        {
            int counter = 1;
            foreach (var data in covidData.Result)
            {
                var reply = DataHelper.FillVaccinesDataReply(data);

                try
                {
                    await replyStream.WriteAsync(reply);
                    _logger.LogInformation(@"Sent reply [{0}] to a client.", counter);

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                catch
                {
                    _logger.LogError("Sending grpc response to a client failed.");
                }
                counter++;
            }
        }
    }
    
    /// <summary>
    /// Sends vaccines metadata to a client
    /// </summary>
    /// <param name="request">gRPC request</param>
    /// <param name="replyStream">gRPC reply stream</param>
    /// <param name="context">ServerCallContext</param>
    /// <returns>Stream of the VaccinesMetadataReplies</returns>
    public override async Task GetVaccinesMetadata(VaccinesMetadataRequest request, IServerStreamWriter<VaccinesMetadataReply> replyStream, ServerCallContext context)
    {
        _logger.LogInformation("Request for vaccination metadata received.");

        var covidData = FileOperations.ReadCovidMetadata(request.Filter, _logger);
        
        if (covidData.Result != null)
        {
            int counter = 1;
            foreach (var data in covidData.Result)
            {
                var reply = DataHelper.FillVaccineMetadataReply(data);

                try
                {
                    await replyStream.WriteAsync(reply);
                    _logger.LogInformation(@"Sent reply [{0}] to a client.", counter);

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                catch
                {
                    _logger.LogError("Sending grpc response to a client failed.");
                }
                counter++;
            }
        }

    }

}
