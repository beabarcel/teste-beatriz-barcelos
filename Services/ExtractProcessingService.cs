using Domain.Models;
using Services.Interfaces;
using System.Xml.Linq;

public class ExtractProcessingService : IExtractProcessingService
{
    private readonly IInvoiceService _invoiceService;

    public ExtractProcessingService(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService ?? throw new ArgumentNullException(nameof(invoiceService));
    }

    public async Task ProcessExtractsAsync()
    {
        if (_invoiceService == null)
        {
            throw new InvalidOperationException("Error: invoice service is not initialized.");
        }

        var serviceResponse = await _invoiceService.GetInvoices();

        var xml = GenerateXml(serviceResponse.Response);
        await SaveXmlAsync(xml);
    }

    private string GenerateXml(IEnumerable<Invoice> invoices)
{
    var xdoc = new XDocument(
        new XElement("Invoices",
            invoices.Select(invoice => 
                new XElement("Invoice",
                    new XElement("Id", invoice.Id),
                    new XElement("Customer", invoice.Customer),
                    new XElement("Performances",
                        invoice.Performances.Select(performance => 
                            new XElement("Performance",
                                new XElement("Id", performance.Id),
                                new XElement("PlayId", performance.PlayId),
                                new XElement("Audience", performance.Audience)
                            )
                        )
                    )
                )
            )
        )
    );

    return xdoc.ToString();
}

    private async Task SaveXmlAsync(string xml)
    {
        var directory = "Extracts";
        var filePath = Path.Combine(directory, $"Extract_{DateTime.Now:yyyyMMddHHmmss}.xml");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllTextAsync(filePath, xml);
    }
}
