using Serilog;
using Serilog.Debugging;
using Serilog.Formatting.Json;

public static class LoggingExtensions
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        try
        {
            SelfLog.Enable(Console.Error);

            var logDirectory = Path.Combine(Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, "Logs");
            
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(new JsonFormatter(), Path.Combine(logDirectory, "logs-.txt"), rollingInterval: RollingInterval.Day) 
                .CreateLogger();

            builder.Host.UseSerilog();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while configuring logging: {ex.Message}");
        }
    }
}