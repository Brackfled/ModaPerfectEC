using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("BasketItems").HasKey(bi => bi.Id);

        builder.Property(bi => bi.Id).HasColumnName("Id").IsRequired();
        builder.Property(bi => bi.BasketId).HasColumnName("BasketId").IsRequired();
        builder.Property(bi => bi.ProductId).HasColumnName("ProductId");
        builder.Property(bi => bi.ProductVariantId).HasColumnName("ProductVariantId");
        builder.Property(bi => bi.ProductAmount).HasColumnName("ProductAmount").IsRequired();
        builder.Property(bi => bi.RemainingAfterDelivery).HasColumnName("RemainingAfterDelivery").IsRequired();
        builder.Property(bi => bi.IsReturned).HasColumnName("IsReturned").IsRequired();
        builder.Property(bi => bi.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(bi => bi.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(bi => bi.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(bi => bi.Basket);
        builder.HasOne(bi => bi.Product).WithMany().HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(bi => bi.ProductVariant).WithMany().HasForeignKey(pv => pv.ProductVariantId).OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(bi => bi.ProductReturns);

        builder.HasQueryFilter(bi => !bi.DeletedDate.HasValue);
    }
}