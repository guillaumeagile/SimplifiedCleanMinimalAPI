using CleanMinimalApi.WebAPI.Extensions;
using Serilog;

public class Program
{
    public static int Main(string[] args)
    {
        var builder = WebApplication
            .CreateBuilder(args)
            .ConfigureApplicationBuilder();

        var app = builder
            .Build()
            .ConfigureApplication();

        try
        {
            Log.Information("Starting host");
            app.Run();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
