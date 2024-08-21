using Data.Theatrical;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Invoice> GetInvoiceById(int id)
        {
            return await _db.Invoices.Include(i => i.Performances).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Invoice>> GetInvoices()
        {
            return await _db.Invoices.Include(i => i.Performances).ToListAsync();
        }
        public async Task<Invoice> CreateInvoice(Invoice invoice)
        {
            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();
            return invoice;
        }

        public async Task<Invoice> PutInvoice(Invoice invoice)
        {
            var existingInvoice = await _db.Invoices
                .Include(i => i.Performances)
                .FirstOrDefaultAsync(i => i.Id == invoice.Id);

            if (existingInvoice == null)
            {
                return existingInvoice;
            }

            existingInvoice.Customer = invoice.Customer;

            _db.Performances.RemoveRange(existingInvoice.Performances);
            existingInvoice.Performances = invoice.Performances;

            await _db.SaveChangesAsync();

            return existingInvoice;
        }
        public async Task<bool> DeleteInvoice(int id)
        {
            var deleteInvoice = await _db.Invoices.FirstOrDefaultAsync(i => i.Id == id);
            if (deleteInvoice != null)
            {
                _db.Invoices.Remove(deleteInvoice);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }


}