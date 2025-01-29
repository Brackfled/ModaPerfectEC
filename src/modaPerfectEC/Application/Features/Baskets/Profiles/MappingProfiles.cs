using Application.Features.Baskets.Commands.Create;
using Application.Features.Baskets.Commands.Delete;
using Application.Features.Baskets.Commands.Update;
using Application.Features.Baskets.Queries.GetById;
using Application.Features.Baskets.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Baskets.Queries.GetFromAuth;

namespace Application.Features.Baskets.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateBasketCommand, Basket>();
        CreateMap<Basket, CreatedBasketResponse>();

        CreateMap<UpdateBasketCommand, Basket>();
        CreateMap<Basket, UpdatedBasketResponse>();

        CreateMap<DeleteBasketCommand, Basket>();
        CreateMap<Basket, DeletedBasketResponse>();

        CreateMap<Basket, GetByIdBasketResponse>();

        CreateMap<Basket, GetListBasketListItemDto>();
        CreateMap<IPaginate<Basket>, GetListResponse<GetListBasketListItemDto>>();

        CreateMap<Basket, GetFromAuthBasketResponse>();
    }
}