using Data.Theatrical;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<IPlayService, PlayService>();
builder.Services.AddDbContext<TheatricalContext>(opt => opt.UseInMemoryDatabase("TheatricalDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Theatrical API");
        options.RoutePrefix = string.Empty;
    });
}

app.MapGet("/invoices", async (IInvoiceService invoiceService) =>
{
    return await invoiceService.GetInvoices();
});

app.MapGet("/invoices/{id}", async (int id, IInvoiceService invoiceService) =>
{
    var getInvoiceById = await invoiceService
    .GetInvoiceById(id);
    return Results.Ok(getInvoiceById);
});

app.MapPost("/invoices", async (IInvoiceService invoiceService, Invoice invoice) =>
{
    var newInvoice = await invoiceService
    .CreateInvoice(invoice);
    return Results.Created($"/invoices/{newInvoice.Id}", newInvoice);
});

app.MapPut("/invoices", async (Invoice updatedInvoice, IInvoiceService invoiceService) =>
{
    var existingInvoice = await invoiceService
    .PutInvoice(updatedInvoice);
    return Results.Ok(existingInvoice);
});

app.MapDelete("/invoices/{id}", async (int id, IInvoiceService invoiceService) =>
{
    var deleteInvoice = await invoiceService
    .DeleteInvoice(id);

    if (deleteInvoice)
    {
        return Results.NoContent();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapGet("/items", async (IItemService itemService) =>
{
    return await itemService.GetItems();
});


app.MapGet("/items/{id}", async (int id, IItemService itemService) =>
{
    var getItemById = await itemService
    .GetItemById(id);
    return Results.Ok(getItemById);
});
app.MapPost("/items", async (IItemService itemService, Item item) =>
{
    var newItem = await itemService
    .CreateItem(item);
    return Results.Created($"/items/{newItem.Id}", newItem);
});

app.MapPut("/items", async (Item updatedItem, IItemService itemService) =>
{
    var existingItem = await itemService
    .PutItem(updatedItem);
    return Results.Ok(existingItem);
});

app.MapDelete("/items/{id}", async (int id, IItemService itemService) =>
{
    var deleteItem = await itemService
    .DeleteItem(id);

    if (deleteItem)
    {
        return Results.NoContent();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapGet("/performances", async (IPerformanceService performanceService) =>
{
    return await performanceService.GetPerformances();
});


app.MapGet("/performances/{id}", async (int id, IPerformanceService performanceService) =>
{
    var getPerformanceById = await performanceService
    .GetPerformanceById(id);
    return Results.Ok(getPerformanceById);
});
app.MapPost("/performances", async (IPerformanceService performanceService, Performance performance) =>
{
    var newPerformance = await performanceService
    .CreatePerformance(performance);
    return Results.Created($"/performances/{newPerformance.Id}", newPerformance);
});

app.MapPut("/performances", async (Performance updatedPerformance, IPerformanceService performanceService) =>
{
    var existingPerformance = await performanceService
    .PutPerformance(updatedPerformance);
    return Results.Ok(existingPerformance);
});

app.MapDelete("/performances/{id}", async (int id, IPerformanceService performanceService) =>
{
    var deletePerformance = await performanceService
    .DeletePerformance(id);

    if (deletePerformance)
    {
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapGet("/plays", async (IPlayService playService) =>
{
    return await playService.GetPlays();
});


app.MapGet("/plays/{id}", async (int id, IPlayService playService) =>
{
    var getPlayById = await playService
    .GetPlayById(id);
    return Results.Ok(getPlayById);
});
app.MapPost("/plays", async (IPlayService playService, Play play) =>
{
    var newPlay = await playService
    .CreatePlay(play);
    return Results.Created($"/plays/{newPlay.Id}", newPlay);
});

app.MapPut("/plays", async (Play updatedPlay, IPlayService playService) =>
{
    var existingPlay = await playService
    .PutPlay(updatedPlay);
    return Results.Ok(existingPlay);
});

app.MapDelete("/plays/{id}", async (int id, IPlayService playService) =>
{
    var deletePlay = await playService
    .DeletePlay(id);

    if (deletePlay)
    {
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();
