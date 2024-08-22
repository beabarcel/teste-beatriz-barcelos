using Domain.Models;
using Services;
using Services.Interfaces;

namespace WebAPI.RouteConfigurators
{
    public class PlayRouteConfigurator
    {
        public static void ConfigureRoutes(WebApplication app)
        {
            app.MapGet("/plays", async (IPlayService playService) =>
            {
                return await playService.GetPlays();
            });

            app.MapGet("/plays/{id}", async (int id, IPlayService playService) =>
            {
                var getPlayById = await playService.GetPlayById(id);
                return getPlayById != null ? Results.Ok(getPlayById) : Results.NotFound();
            });

            app.MapPost("/plays", async (IPlayService playService, Play play) =>
            {
                var newPlay = await playService.CreatePlay(play);
                return Results.Created($"/plays/{newPlay.Response.Id}", newPlay);
            });

            app.MapPut("/plays", async (Play updatedPlay, IPlayService playService) =>
            {
                var existingPlay = await playService.PutPlay(updatedPlay);
                return existingPlay.Response != null ? Results.Ok(existingPlay) : Results.NotFound(existingPlay.Error);
            });

            app.MapDelete("/plays/{id}", async (int id, IPlayService playService) =>
            {
                var deletePlay = await playService.DeletePlay(id);
                return deletePlay.Response ? Results.NoContent() : Results.NotFound(deletePlay.Error);
            });
        }
    }
}