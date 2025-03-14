﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class ReturnImageConfiguration : IEntityTypeConfiguration<ReturnImage>
{
    public void Configure(EntityTypeBuilder<ReturnImage> builder)
    {
        builder.HasOne(ri => ri.ProductReturn);
    }
}
