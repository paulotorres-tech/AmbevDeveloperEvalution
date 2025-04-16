using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal DiscountPercent { get; private set; }

        public decimal TotalAmount => CalculateTotalAmount();

        private SaleItem() { } // for EF

        public SaleItem(int productId, int quantity, decimal unitPrice)
        {
            if (quantity < 1)
                throw new DomainException("Quantity must be at least 1.");
            if (quantity > 20)
                throw new DomainException("Cannot sell more than 20 identical items.");

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountPercent = CalculateDiscount(quantity);
        }

        private decimal CalculateDiscount(int quantity)
        {
            if (quantity >= 10) return 0.2m; // 20% discount for 10 or more items
            if (quantity >= 4) return 0.1m; // 10% discount for 4 to 9 items

            return 0m; // No discount for less than 4 items
        }

        private decimal CalculateTotalAmount()
        {
            decimal totalPrice = UnitPrice * Quantity;
            decimal discountAmount = totalPrice * DiscountPercent;
            return totalPrice - discountAmount;
        }
    }
}
