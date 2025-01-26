using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products").HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.CategoryId).HasColumnName("CategoryId").IsRequired();
        builder.Property(p => p.SubCategoryId).HasColumnName("SubCategoryId").IsRequired();
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
        builder.Property(p => p.Price).HasColumnName("Price").IsRequired();
        builder.Property(p => p.PriceUSD).HasColumnName("PriceUSD").IsRequired();
        builder.Property(p => p.Description).HasColumnName("Description").IsRequired();
        builder.Property(p => p.ProductState).HasColumnName("ProductState").IsRequired();
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(p => p.Name, name:"UK_Products_Name").IsUnique();

        builder.HasOne(p => p.Category).WithMany(sc => sc.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.SubCategory).WithMany(sc => sc.Products).HasForeignKey(p => p.SubCategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(p => p.ProductVariants).WithOne(pv => pv.Product).HasForeignKey(pv => pv.ProductId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.ProductImages).WithOne(pv => pv.Product).HasForeignKey(pv => pv.ProductId).OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
    }
}