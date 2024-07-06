using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace OpenTelemetry.Shared
{
    public static class OpenTelemetryExtensions
    {
        public static void AddOpenTelemetryExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OpenTelemetryConstants>(configuration.GetSection("OpenTelemetry"));

            OpenTelemetryConstants openTelemetryConstant = (configuration.GetSection("OpenTelemetry").Get<OpenTelemetryConstants>())!;

            services.AddOpenTelemetry()
                .WithTracing(options =>
                {
                    options.AddSource(openTelemetryConstant.ActivitySourceName)
                    .ConfigureResource(resource =>
                    {
                        resource.AddService(serviceName: openTelemetryConstant.ServiceName, serviceVersion: openTelemetryConstant.ServiceVersion);
                    });

                    options.AddAspNetCoreInstrumentation(ancOptions =>
                    {
                        ancOptions.Filter = (context) =>
                        {
                            if (!string.IsNullOrEmpty(context.Request.Path.Value))
                                return context.Request.Path.Value.Contains("api", StringComparison.InvariantCulture);

                            return false;
                        };

                        ancOptions.RecordException = true;

                        ancOptions.EnrichWithException = (activity, exception) =>
                        {
                        };
                    });

                    options.AddEntityFrameworkCoreInstrumentation(efOptions =>
                    {
                        efOptions.SetDbStatementForText = true;
                        efOptions.SetDbStatementForStoredProcedure = true;
                        efOptions.EnrichWithIDbCommand = (activity, dbCommand) =>
                        {

                        };
                    });

                    options.AddOtlpExporter();
                });

            ActivitySourceProvider.Source = new ActivitySource(openTelemetryConstant.ActivitySourceName);
        }
    }
}
