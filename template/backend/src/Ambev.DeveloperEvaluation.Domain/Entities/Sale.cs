using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; private set; } = string.Empty;
        public DateTime SaleDate { get; private set; }

        public int CustomerId { get; private set; }
        public string CustomerName { get; private set; } = string.Empty; // denormalized field

        public int BranchId { get; private set; }
        public string BranchName { get; private set; } = string.Empty; // denormalized field
        
        public bool IsCancelled { get; private set; }

        public readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();
        
        public decimal TotalAmount => _items.Sum(item => item.TotalAmount);

        private Sale() { } // for EF

        public Sale(string saleNumber, DateTime saleDate, int customerId, string customerName,  int branchId, string branchName)
        {
            if (string.IsNullOrWhiteSpace(saleNumber))
                throw new DomainException("Sale number cannot be empty.");

            SaleNumber = saleNumber;
            SaleDate = saleDate;

            CustomerId = customerId;
            CustomerName = customerName;

            BranchId = branchId;
            BranchName = branchName;

            IsCancelled = false;

            AddDomainEvent(new SaleCreatedEvent(this));
        }

        public void AddItem(int productId, int quantity, decimal unitPrice)
        {
            if (IsCancelled)
                throw new DomainException("Cannot add items to a cancelled sale.");

            var item = new SaleItem(productId, quantity, unitPrice);
            _items.Add(item);
        }

        public void CancelSale()
        {
            if (IsCancelled)
                throw new DomainException("Sale is already cancelled.");

            IsCancelled = true;
            AddDomainEvent(new SaleCancelledEvent(this.Id));
        }

        // Events
        private readonly List<object> _domainEvents = new();
        public IReadOnlyList<object> DomainEvents => _domainEvents.AsReadOnly();
        protected void AddDomainEvent(object domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
