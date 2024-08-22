using Data.Theatrical;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;
using WebAPI.RouteConfigurators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<IPlayService, PlayService>();
builder.Services.AddScoped<IExtractProcessingService, ExtractProcessingService>();
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

InvoiceRouteConfigurator.ConfigureRoutes(app);
ItemRouteConfigurator.ConfigureRoutes(app);
PerformanceRouteConfigurator.ConfigureRoutes(app);
PlayRouteConfigurator.ConfigureRoutes(app);
ExtractProcessingRouteConfigurator.ConfigureRoutes(app);


app.Run();
