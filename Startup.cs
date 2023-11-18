using System.Security.Cryptography.X509Certificates;
using MedicalService.Services;
using Microsoft.AspNetCore.Authentication.Certificate;

namespace MedicalService;

public class Startup
{

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGrpc(options => 
        {
            //options.MaxConcurrentCalls = 100; by default
            options.EnableDetailedErrors = true;
        });

        services.AddAuthentication().AddCertificate(opt =>
        {
            opt.AllowedCertificateTypes = CertificateTypes.SelfSigned;
            opt.RevocationMode = X509RevocationMode.NoCheck;
            opt.Events = new CertificateAuthenticationEvents()
            {
                OnCertificateValidated = ctx =>
                {
                    ctx.Success();
                    return Task.CompletedTask;
                }
            };
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
            });

        }
}