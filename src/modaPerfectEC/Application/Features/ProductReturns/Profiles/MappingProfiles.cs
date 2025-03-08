using Application.Features.ProductReturns.Commands.Create;
using Application.Features.ProductReturns.Commands.Delete;
using Application.Features.ProductReturns.Commands.Update;
using Application.Features.ProductReturns.Queries.GetById;
using Application.Features.ProductReturns.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ProductReturns.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductReturnCommand, ProductReturn>();
        CreateMap<ProductReturn, CreatedProductReturnResponse>();

        CreateMap<UpdateProductReturnCommand, ProductReturn>();
        CreateMap<ProductReturn, UpdatedProductReturnResponse>();

        CreateMap<DeleteProductReturnCommand, ProductReturn>();
        CreateMap<ProductReturn, DeletedProductReturnResponse>();

        CreateMap<ProductReturn, GetByIdProductReturnResponse>();

        CreateMap<ProductReturn, GetListProductReturnListItemDto>();
        CreateMap<IPaginate<ProductReturn>, GetListResponse<GetListProductReturnListItemDto>>();
    }
}