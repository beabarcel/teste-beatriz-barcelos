using Data.Theatrical;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Common;
using Services.Interfaces;

namespace Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly TheatricalContext _db;

        public InvoiceService(TheatricalContext db)
        {
            _db = db;
        }

        public async Task<ServiceResponse<Invoice>> GetInvoiceById(int id)
        {
            var invoice = await _db.Invoices
                .Include(i => i.Performances)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return new ServiceResponse<Invoice>("Invoice not found");
            }

            return new ServiceResponse<Invoice>(invoice);
        }

        public async Task<ServiceResponse<List<Invoice>>> GetInvoices()
        {
            var invoices = await _db.Invoices
                .Include(i => i.Performances)
                .ToListAsync();

            return new ServiceResponse<List<Invoice>>(invoices);
        }

        public async Task<ServiceResponse<Invoice>> CreateInvoice(Invoice invoice)
        {
            var existingInvoice = await _db.Invoices
                .FirstOrDefaultAsync(i => i.Id == invoice.Id);

            if (existingInvoice != null)
            {
                return new ServiceResponse<Invoice>("Invoice already exists");
            }

            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();
            return new ServiceResponse<Invoice>(invoice);
        }

        public async Task<ServiceResponse<Invoice>> PutInvoice(Invoice invoice)
        {
            var existingInvoice = await _db.Invoices.FindAsync(invoice.Id);

            if (existingInvoice == null)
            {
                return new ServiceResponse<Invoice>("Invoice not found");
            }
            _db.Entry(existingInvoice).CurrentValues.SetValues(invoice);

            await _db.SaveChangesAsync();

            return new ServiceResponse<Invoice>(existingInvoice);
        }

        public async Task<ServiceResponse<bool>> DeleteInvoice(int id)
        {
            var deleteInvoice = await _db.Invoices.FirstOrDefaultAsync(i => i.Id == id);
            if (deleteInvoice != null)
            {
                _db.Invoices.Remove(deleteInvoice);
                await _db.SaveChangesAsync();
                return new ServiceResponse<bool>(true);
            }

            return new ServiceResponse<bool>("Invoice not found");
        }
    }
}