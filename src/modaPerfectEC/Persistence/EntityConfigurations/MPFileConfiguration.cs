using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class MPFileConfiguration : IEntityTypeConfiguration<MPFile>
{
    public void Configure(EntityTypeBuilder<MPFile> builder)
    {
        builder.ToTable("MPFile").HasKey(mpf => mpf.Id);

        builder.Property(mpf => mpf.Id).HasColumnName("Id").IsRequired();
        builder.Property(mpf => mpf.FileName).HasColumnName("FileName").IsRequired();
        builder.Property(mpf => mpf.FilePath).HasColumnName("FilePath").IsRequired();
        builder.Property(mpf => mpf.FileUrl).HasColumnName("FileUrl").IsRequired();
        builder.Property(mpf => mpf.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(mpf => mpf.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(mpf => mpf.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(mpf => mpf.FileName, name:"UK_MPFile_FileName").IsUnique();

        builder.HasDiscriminator<string>("FileType")
            .HasValue<MPFile>("MPFile")
            .HasValue<ProductImage>("ProductImage")
            .HasValue<CollectionVideo>("CollectionVideo")
            .HasValue<ReturnImage>("ReturnImage");
    }
}
