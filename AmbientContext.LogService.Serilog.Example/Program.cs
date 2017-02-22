using System;
using AmbientContext.LogService.Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;
using StatsdClient;

namespace AmbientContext.LogService.Serilog.Example
{
    class Program
    {
        private static readonly AmbientLogService Logger = new AmbientLogService();

        static void Main(string[] args)
        {
            ConfigureSerilog();
            ConfigureStatsDLoggingHandler();

            Logger.Information("Application starting");

            try
            {
                new NumberAdder().Add(3,5);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "Exception from application");
            }

            Console.ReadLine();
        }

        private static void ConfigureStatsDLoggingHandler()
        {
            AmbientLogService.AddLogHandler(
                new StatsdLogHandler(
                    new Statsd("localhost", 12000)));
        }

        private static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.LiterateConsole()
                .CreateLogger();
        }
    }
}
