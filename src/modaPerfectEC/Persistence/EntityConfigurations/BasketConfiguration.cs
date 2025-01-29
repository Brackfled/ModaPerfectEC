using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.ToTable("Baskets").HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(b => b.TotalPrice).HasColumnName("TotalPrice").IsRequired();
        builder.Property(b => b.IsOrderBasket).HasColumnName("IsOrderBasket").IsRequired();
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(b => b.User);
        builder.HasMany(b => b.BasketItems).WithOne(bi => bi.Basket).HasForeignKey(bi => bi.BasketId).OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
    }
}