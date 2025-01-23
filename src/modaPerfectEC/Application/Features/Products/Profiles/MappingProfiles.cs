using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Products.Queries.GetAllByFiltered;
using Application.Features.Products.Queries.GetListByDynamic;
using Application.Features.Products.Queries.GetListByShowCase;

namespace Application.Features.Products.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreatedProductResponse>();

        CreateMap<UpdateProductCommand, Product>();
        CreateMap<Product, UpdatedProductResponse>();
        CreateMap<UpdateProductRequest, Product>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null && (srcMember is not int || (int?)srcMember != 0)))
            ;

        CreateMap<DeleteProductCommand, Product>();
        CreateMap<Product, DeletedProductResponse>();

        CreateMap<Product, GetByIdProductResponse>();

        CreateMap<Product, GetListProductListItemDto>();
        CreateMap<IPaginate<Product>, GetListResponse<GetListProductListItemDto>>();

        CreateMap<Product, GetAllByFilteredProductListItemDto>().ReverseMap();
        CreateMap<ICollection<Product>, ICollection<GetAllByFilteredProductListItemDto>>()
            .ConvertUsing((src, dest, context) =>
                src.Select(product => context.Mapper.Map<GetAllByFilteredProductListItemDto>(product)).ToList());

        CreateMap<Product, GetListByDynamicProductListItemDto>();
        CreateMap<IPaginate<Product>, GetListResponse<GetListByDynamicProductListItemDto>>();

        CreateMap<Product, GetListByShowCaseProductListItemDto>();
        CreateMap<ICollection<Product>, ICollection<GetListByShowCaseProductListItemDto>>()
            .ConvertUsing((src, dest, context) =>
                src.Select(product => context.Mapper.Map<GetListByShowCaseProductListItemDto>(product)).ToList());
    }
}