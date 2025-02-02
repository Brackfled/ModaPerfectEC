using Application.Features.ProductVariants.Commands.Create;
using Application.Features.ProductVariants.Commands.Delete;
using Application.Features.ProductVariants.Commands.Update;
using Application.Features.ProductVariants.Queries.GetById;
using Application.Features.ProductVariants.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.ProductVariants.Commands.UpdateStockAmount;

namespace Application.Features.ProductVariants.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductVariantCommand, ProductVariant>();
        CreateMap<ProductVariant, CreatedProductVariantResponse>();

        CreateMap<UpdateProductVariantCommand, ProductVariant>();
        CreateMap<ProductVariant, UpdatedProductVariantResponse>();
        CreateMap<UpdateProductVariantRequest, ProductVariant>()
            .ForMember(dest => dest.Sizes, opt => opt.Condition(src => src.Sizes != null))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                srcMember != null &&
                (srcMember is not int || (int?)srcMember != 0) &&
                (srcMember is not byte[] || ((byte[])srcMember).Length > 0)
                ))
            ;

        CreateMap<DeleteProductVariantCommand, ProductVariant>();
        CreateMap<ProductVariant, DeletedProductVariantResponse>();

        CreateMap<ProductVariant, GetByIdProductVariantResponse>();

        CreateMap<ProductVariant, GetListProductVariantListItemDto>();
        CreateMap<IPaginate<ProductVariant>, GetListResponse<GetListProductVariantListItemDto>>();

        CreateMap<ProductVariant, UpdatedStockAmountProductVariantResponse>();
    }
}