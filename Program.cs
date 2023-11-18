using System.Net;

namespace MedicalService;

public class Program
{

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.ConfigureKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = 10;
                    options.Listen(IPAddress.Any, 7272, serverOptions =>
                    {
                        serverOptions.UseHttps("C:\\Windows\\System32\\aspNetCoreHttps.crt", "C:\\Windows\\System32\\aspNetCoreHttps.key");
                    });
                });
            });
}