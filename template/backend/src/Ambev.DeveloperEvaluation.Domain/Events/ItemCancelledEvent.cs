using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class ItemCancelledEvent
    {
        public Guid SaleId { get; }
        public int ProductId { get; }

        public ItemCancelledEvent(Guid saleId, int productId)
        {
            SaleId = saleId;
            ProductId = productId;
        }
    }
}
