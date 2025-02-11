using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class CollectionVideoConfiguration : IEntityTypeConfiguration<CollectionVideo>
{
    public void Configure(EntityTypeBuilder<CollectionVideo> builder)
    {
        builder.Property(cv => cv.CollectionName).HasColumnName("CollectionName").IsRequired();
        builder.Property(cv => cv.CollectionState).HasColumnName("CollectionState").IsRequired();

        builder.HasIndex(cv => cv.CollectionName).IsUnique();
    }
}
