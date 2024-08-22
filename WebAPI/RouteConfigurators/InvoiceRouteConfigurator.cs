using Domain.Models;
using Services.Interfaces;

namespace WebAPI.RouteConfigurators
{
    public class InvoiceRouteConfigurator
    {
        public static void ConfigureRoutes(WebApplication app)
        {
            app.MapGet("/invoices", async (IInvoiceService invoiceService) =>
            {
                return await invoiceService.GetInvoices();
            });

            app.MapGet("/invoices/{id}", async (int id, IInvoiceService invoiceService) =>
            {
                var getInvoiceById = await invoiceService.GetInvoiceById(id);
                return getInvoiceById != null ? Results.Ok(getInvoiceById) : Results.NotFound();
            });

            app.MapPost("/invoices", async (IInvoiceService invoiceService, Invoice invoice) =>
            {
                var newInvoice = await invoiceService.CreateInvoice(invoice);
                return Results.Created($"/invoices/{newInvoice.Response.Id}", newInvoice);
            });

            app.MapPut("/invoices", async (Invoice updatedInvoice, IInvoiceService invoiceService) =>
            {
                var existingInvoice = await invoiceService.PutInvoice(updatedInvoice);
                return existingInvoice.Response != null ? Results.Ok(existingInvoice) : Results.NotFound(existingInvoice.Error);
            });

            app.MapDelete("/invoices/{id}", async (int id, IInvoiceService invoiceService) =>
            {
                var deleteInvoice = await invoiceService.DeleteInvoice(id);
                return deleteInvoice.Response ? Results.NoContent() : Results.NotFound(deleteInvoice.Error);
            });
        }
    }
}
