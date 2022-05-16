using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notadesigner.Pomodour.Core;
using Serilog;

namespace Notadesigner.Pomodour.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static async Task Main(string[] args)
        {
            /// Initialize the application logging framework
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "appsettings.json");
            var configuration = configurationBuilder.Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                /// Attempt to initialize the service
                Log.Information("Starting Pomodour process");

                var builder = Host.CreateDefaultBuilder(args);
                Log.Verbose("Launching {serviceName}", nameof(PomoEngine));
                builder.ConfigureServices((_, services) =>
                    {
                        services.AddSingleton<PomoEngine>()
                            .AddSingleton<NotificationsQueue>()
                            .AddSingleton<MainForm>();
                    })
                    .UseSerilog();
                var host = builder.Build();

                var cts = new CancellationTokenSource();
                var engine = host.Services.GetRequiredService<PomoEngine>();
                await engine.StartAsync(cts.Token);

                ApplicationConfiguration.Initialize();
                var form = host.Services.GetRequiredService<MainForm>();
                Application.Run(form);
            }
            catch (ApplicationException exception)
            {
                Log.Fatal(exception, "The service was not launched correctly.");
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "The service failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}