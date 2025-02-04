using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Queries.GetById;
using Application.Features.Orders.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Orders.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, CreatedOrderResponse>();

        CreateMap<UpdateOrderCommand, Order>();
        CreateMap<Order, UpdatedOrderResponse>();

        CreateMap<DeleteOrderCommand, Order>();
        CreateMap<Order, DeletedOrderResponse>();

        CreateMap<Order, GetByIdOrderResponse>()
            .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.BasketBasketItems, opt => opt.MapFrom(src => src.Basket.BasketItems));

        CreateMap<BasketItem, BasketItemDto>();

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(i => i.FileUrl).ToList()));

        CreateMap<ProductVariant, ProductVariantDto>();


        CreateMap<Order, GetListOrderListItemDto>();
        CreateMap<IPaginate<Order>, GetListResponse<GetListOrderListItemDto>>();
    }
}