using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductReturnConfiguration : IEntityTypeConfiguration<ProductReturn>
{
    public void Configure(EntityTypeBuilder<ProductReturn> builder)
    {
        builder.ToTable("ProductReturns").HasKey(pr => pr.Id);

        builder.Property(pr => pr.Id).HasColumnName("Id").IsRequired();
        builder.Property(pr => pr.BasketItemId).HasColumnName("BasketItemId").IsRequired();
        builder.Property(pr => pr.OrderId).HasColumnName("OrderId").IsRequired();
        builder.Property(pr => pr.Description).HasColumnName("Description").IsRequired();
        builder.Property(pr => pr.ReturnState).HasColumnName("ReturnState").IsRequired();
        builder.Property(pr => pr.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pr => pr.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pr => pr.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(pr => !pr.DeletedDate.HasValue);

        builder.HasOne(pr => pr.BasketItem);
        builder.HasOne(pr => pr.Order);
        builder.HasMany(pr => pr.ReturnImages).WithOne(pr => pr.ProductReturn).HasForeignKey(pr => pr.ProductReturnId).OnDelete(deleteBehavior:DeleteBehavior.Cascade);
    }
}