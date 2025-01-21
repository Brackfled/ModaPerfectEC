using Application.Features.MPFile.Commands.DeleteProductImage;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Profiles;
public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<ProductImage, DeletedProductImageResponse>();
    }
}
