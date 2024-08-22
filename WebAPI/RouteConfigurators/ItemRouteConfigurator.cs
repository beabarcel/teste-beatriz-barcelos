using Domain.Models;
using Services.Interfaces;

namespace WebAPI.RouteConfigurators
{
    public class ItemRouteConfigurator
    {
        public static void ConfigureRoutes(WebApplication app)
        {
            app.MapGet("/items", async (IItemService itemService) =>
            {
                return await itemService.GetItems();
            });

            app.MapGet("/items/{id}", async (int id, IItemService itemService) =>
            {
                var getItemById = await itemService.GetItemById(id);
                return getItemById != null ? Results.Ok(getItemById) : Results.NotFound();
            });

            app.MapPost("/items", async (IItemService itemService, Item item) =>
            {
                var newItem = await itemService.CreateItem(item);
                return Results.Created($"/items/{newItem.Response.Id}", newItem);
            });

            app.MapPut("/items", async (Item updatedItem, IItemService itemService) =>
            {
                var existingItem = await itemService.PutItem(updatedItem);
                return existingItem.Response != null ? Results.Ok(existingItem) : Results.NotFound(existingItem.Error);
            });

            app.MapDelete("/items/{id}", async (int id, IItemService itemService) =>
            {
                var deleteItem = await itemService.DeleteItem(id);
                return deleteItem.Response ? Results.NoContent() : Results.NotFound(deleteItem.Error);
            });
        }
    }
}
