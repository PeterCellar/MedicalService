using System.Security.Cryptography.X509Certificates;
using MedicalService.Services;
using Microsoft.AspNetCore.Authentication.Certificate;

namespace MedicalService;

public class Startup
{

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGrpc(x => x.EnableDetailedErrors = true);
        services.AddAuthentication().AddCertificate(opt =>
        {
            opt.AllowedCertificateTypes = CertificateTypes.SelfSigned;
            opt.RevocationMode = X509RevocationMode.NoCheck; // Self-Signed Certs (Development)
            opt.Events = new CertificateAuthenticationEvents()
            {
                OnCertificateValidated = ctx =>
                {
                    // Write additional Validation  
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
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello");
                });
            });

        }
}