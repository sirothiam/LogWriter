using LoggerLibrary;
using System.Configuration;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

namespace LogWriter
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {

             Type myType = typeof(LogWriter.Program);
                //var message = myType.ToString() + "|" + Level.FATAL.ToString() + "|" + "Object reference not set to an object";
                var message = myType.ToString() + "|" + Level.INFO.ToString() + "|" + "Fatal error. Please connect with your system admin";

              

            // Setting up dependency injection
            var serviceCollection = new ServiceCollection();

            //Configure the service collection for DI.
                ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Get an instance of the service
            var objService = serviceProvider.GetService<LoggerService>();

                
            if((!string.IsNullOrEmpty(message)) && (message.Split('|').Length == 3))
                { 
             //********************** Start Log Error To Console (Service :- With Configuration) ***************
                
                objService?.LogActivity(message);

             //********************** End Log Error To Console (Service :- With Configuration) *****************




             //********************** Start Log Error To File. ***************


                //LogHelper.Log(LogTarget.File, message);
                //Console.WriteLine("Error logged to File");

             //********************** End Start Log Error To File. ***************



             //********************** Start Log Error To Database. ***************


                //LogHelper.Log(LogTarget.Database, message);
                //Console.WriteLine("Error logged to Database");

                    //********************** Start Log Error To Database. ***************


               Console.ReadLine();
             }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddDebug(); // Log to debug (debug window in Visual Studio or any debugger attached)
                config.AddConsole(); // Log to console (colored !)
            })
            .Configure<LoggerFilterOptions>(options =>
            {
                //options.AddFilter<DebugLoggerProvider>(null /* category*/ , LogLevel.Information /* min level */);
                //options.AddFilter<ConsoleLoggerProvider>(null  /* category*/ , LogLevel.Warning /* min level */);
                options.AddFilter<ConsoleLoggerProvider>(null  /* category*/ , LogLevel.Debug /* min level */);
            })
            .AddTransient<LoggerService>(); // Register service from the library
        }
    }
}