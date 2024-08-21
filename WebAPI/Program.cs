using Data.Theatrical;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddDbContext<TheatricalContext>(opt => opt.UseInMemoryDatabase("TheatricalDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

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

app.MapGet("/performances", async (TheatricalContext db) =>
    await db.Performances.ToListAsync());

app.MapPost("/performances", async (TheatricalContext db, Performance performance) =>
{
    db.Performances.Add(performance);
    await db.SaveChangesAsync();
    return Results.Created($"/performances/{performance.Id}", performance);
});

app.MapPut("/performances/{id}", async (int id, TheatricalContext db, Performance updatedPerformance) =>
{
    var existingPerformance = await db.Performances.FindAsync(id);
    if (existingPerformance == null)
    {
        return Results.NotFound();
    }

    existingPerformance.PlayId = updatedPerformance.PlayId;
    existingPerformance.Audience = updatedPerformance.Audience;

    await db.SaveChangesAsync();
    return Results.Ok(existingPerformance);
});

app.MapDelete("/performances/{id}", async (int id, TheatricalContext db) =>
{
    if (await db.Performances.FindAsync(id) is Performance performance)
    {
        db.Performances.Remove(performance);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.MapGet("/plays", async (TheatricalContext db) =>
    await db.Plays.ToListAsync());

app.MapPost("/plays", async (TheatricalContext db, Play play) =>
{
    db.Plays.Add(play);
    await db.SaveChangesAsync();
    return Results.Created($"/plays/{play.Id}", play);
});

app.MapPut("/plays/{id}", async (int id, TheatricalContext db, Play updatedPlay) =>
{
    var existingPlay = await db.Plays.FindAsync(id);
    if (existingPlay == null)
    {
        return Results.NotFound();
    }

    existingPlay.Name = updatedPlay.Name;
    existingPlay.Lines = updatedPlay.Lines;
    existingPlay.Type = updatedPlay.Type;

    await db.SaveChangesAsync();
    return Results.Ok(existingPlay);
});

app.MapDelete("/plays/{id}", async (int id, TheatricalContext db) =>
{
    if (await db.Plays.FindAsync(id) is Play play)
    {
        db.Plays.Remove(play);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();
