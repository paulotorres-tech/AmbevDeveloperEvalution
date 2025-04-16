using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public  class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
        }

        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<bool> CancelByIdAsync(Guid saleId, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(saleId, cancellationToken);
            if (sale == null) return false;

            sale.CancelSale();
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> CancelBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        {
            var sale = await GetBySaleNumberAsync(saleNumber, cancellationToken);
            if (sale == null) return false;

            sale.CancelSale();
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid saleId, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(saleId, cancellationToken);
            if (sale == null) return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        {
            var sale = await GetBySaleNumberAsync(saleNumber, cancellationToken);
            if (sale == null) return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
