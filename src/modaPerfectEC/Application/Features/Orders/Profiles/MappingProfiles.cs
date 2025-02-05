using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Queries.GetById;
using Application.Features.Orders.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Orders.Queries.GetListAll;
using Application.Features.Orders.Queries.GetListFromAuth;

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
            .ForMember(o => o.UserFirstName, memberOptions: o => o.MapFrom(o => o.User!.FirstName))
            ;

        CreateMap<Order, GetListOrderListItemDto>();
        CreateMap<IPaginate<Order>, GetListResponse<GetListOrderListItemDto>>();

        CreateMap<Order, GetListAllOrderListItemDto>();

        CreateMap<Order, GetListFromAuthOrderListItemDto>();
    }
}