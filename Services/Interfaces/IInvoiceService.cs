using Domain.Models;
using Services.Common;

namespace Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<ServiceResponse<List<Invoice>>> GetInvoices();
        Task<ServiceResponse<Invoice>> GetInvoiceById(int id);
        Task<ServiceResponse<Invoice>> CreateInvoice(Invoice invoice);
        Task<ServiceResponse<Invoice>> PutInvoice(Invoice invoice);
        Task<ServiceResponse<bool>> DeleteInvoice(int id);

    }
}
