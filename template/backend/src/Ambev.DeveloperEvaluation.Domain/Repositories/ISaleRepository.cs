using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);
        
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
        
        Task<bool> CancelByIdAsync(Guid saleId, CancellationToken cancellationToken = default);
        Task<bool> CancelBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);

        Task<bool> DeleteByIdAsync(Guid saleId, CancellationToken cancellationToken = default);
        Task<bool> DeleteBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);
    }
}
