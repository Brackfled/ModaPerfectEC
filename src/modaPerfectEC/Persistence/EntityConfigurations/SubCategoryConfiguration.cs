using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.ToTable("SubCategories").HasKey(sc => sc.Id);

        builder.Property(sc => sc.Id).HasColumnName("Id").IsRequired();
        builder.Property(sc => sc.CategoryId).HasColumnName("CategoryId").IsRequired();
        builder.Property(sc => sc.Name).HasColumnName("Name").IsRequired();
        builder.Property(sc => sc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sc => sc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sc => sc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(sc => sc.Name, name:"UK_SubCategories_Name").IsUnique();

        builder.HasOne(sc => sc.Category);
        builder.HasMany(sc => sc.Products).WithOne(p => p.SubCategory).HasForeignKey(p => p.SubCategoryId).OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(sc => !sc.DeletedDate.HasValue);
    }
}