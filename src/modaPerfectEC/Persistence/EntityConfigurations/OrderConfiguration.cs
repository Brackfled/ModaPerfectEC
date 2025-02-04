using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders").HasKey(o => o.Id);

        builder.Property(o => o.Id).HasColumnName("Id").IsRequired();
        builder.Property(o => o.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(o => o.BasketId).HasColumnName("BasketId").IsRequired();
        builder.Property(o => o.OrderNo).HasColumnName("OrderNo").IsRequired();
        builder.Property(o => o.OrderPrice).HasColumnName("OrderPrice").IsRequired();
        builder.Property(o => o.TrackingNumber).HasColumnName("TrackingNumber");
        builder.Property(o => o.IsInvoiceSended).HasColumnName("IsInvoiceSended").IsRequired();
        builder.Property(o => o.OrderState).HasColumnName("OrderState").IsRequired();
        builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(o => o.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(o => o.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(o => o.Basket);
        builder.HasOne(o => o.User);

        builder.HasQueryFilter(o => !o.DeletedDate.HasValue);
    }
}