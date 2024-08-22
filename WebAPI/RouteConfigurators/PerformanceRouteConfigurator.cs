using Domain.Models;
using Services.Interfaces;

namespace WebAPI.RouteConfigurators
{
    public class PerformanceRouteConfigurator
    {
        public static void ConfigureRoutes(WebApplication app)
        {
            app.MapGet("/performances", async (IPerformanceService performanceService) =>
            {
                return await performanceService.GetPerformances();
            });

            app.MapGet("/performances/{id}", async (int id, IPerformanceService performanceService) =>
            {
                var getPerformanceById = await performanceService.GetPerformanceById(id);
                return getPerformanceById != null ? Results.Ok(getPerformanceById) : Results.NotFound();
            });

            app.MapPost("/performances", async (IPerformanceService performanceService, Performance performance) =>
            {
                var newPerformance = await performanceService.CreatePerformance(performance);
                if (newPerformance.Response != null)
                {
                    return Results.Created($"/performances/{newPerformance.Response.Id}", newPerformance);
                }
                return Results.BadRequest(newPerformance.Error);
            });

            app.MapPut("/performances", async (Performance updatedPerformance, IPerformanceService performanceService) =>
            {
                var result = await performanceService.PutPerformance(updatedPerformance);
                if (result.Response != null)
                {
                    return Results.Ok(result.Response);
                }

                return Results.NotFound(result.Error);
            });


            app.MapDelete("/performances/{id}", async (int id, IPerformanceService performanceService) =>
            {
                var deletePerformance = await performanceService.DeletePerformance(id);
                return deletePerformance.Response ? Results.NoContent() : Results.NotFound(deletePerformance.Error);
            });
        }
    }
}