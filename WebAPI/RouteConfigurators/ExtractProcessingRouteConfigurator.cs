using Services.Interfaces;

namespace WebAPI.RouteConfigurators
{
    public class ExtractProcessingRouteConfigurator
    {
        public static void ConfigureRoutes(WebApplication app)
        {
            app.MapPost("/process-extracts", async (IExtractProcessingService extractProcessingService) =>
            {
                await extractProcessingService.ProcessExtractsAsync();
                return Results.Ok("Processed extract.");
            });
        }
    }
}