using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            // table name, primary key and unique index
            builder.ToTable("Sales");
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.SaleNumber).IsUnique();

            // properties constraints
            builder.Property(s => s.SaleNumber)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(s => s.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.BranchName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.SaleDate)
                .IsRequired();

            // Owns the SaleItem collection as value object
            builder.OwnsMany(s => s.Items, item =>
            {
                item.WithOwner().HasForeignKey("SaleId");

                item.ToTable("SaleItems");

                // Shadow property to make each item identifiable
                item.Property<int>("Id");
                item.HasKey("Id");

                item.Property(i => i.ProductId).IsRequired();
                item.Property(i => i.Quantity).IsRequired();
                item.Property(i => i.UnitPrice).HasColumnType("decimal(10,2)").IsRequired();
                item.Property(i => i.DiscountPercent).HasColumnType("decimal(5,2)").IsRequired();
            });
        }
    }
}
