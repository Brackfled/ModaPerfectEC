using Application.Features.MPFile.Commands.DeleteCollectionVideo;
using Application.Features.MPFile.Commands.DeleteProductImage;
using Application.Features.MPFile.Commands.UpdateCollectionStateCollectionVideo;
using Application.Features.MPFile.Queries.GetAllByActiveCollectionVideo;
using Application.Features.MPFile.Queries.GetAllCollectionVideo;
using Application.Features.MPFile.Queries.GetById;
using Application.Features.MPFile.Queries.GetListByProductId;
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

        CreateMap<ProductImage, GetByIdProductImageResponse>();

        CreateMap<ProductImage, GetListByProductIdProductImageListItemDto>();

        CreateMap<CollectionVideo, DeletedCollectionVideoResponse>();

        CreateMap<CollectionVideo, GetAllCollectionVideoListItemDto>();

        CreateMap<CollectionVideo, UpdatedCollectionStateCollectionVideoResponse>();

        CreateMap<CollectionVideo, GetAllByActiveCollectionVideoListItemDto>();
    }
}
