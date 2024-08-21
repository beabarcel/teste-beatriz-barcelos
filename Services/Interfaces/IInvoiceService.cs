using Domain.Models;

namespace Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<Invoice>> GetInvoices();
        Task<Invoice> GetInvoiceById(int id);
        Task<Invoice> CreateInvoice(Invoice invoice);
        Task<Invoice> PutInvoice(Invoice invoice);
        Task<bool> DeleteInvoice(int id);

    }
}
