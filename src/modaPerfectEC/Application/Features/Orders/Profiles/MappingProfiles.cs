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
            .ForMember(o => o.UserReference, opt => opt.MapFrom(o => o.User.Reference))
            .ForMember(o => o.UserFirstName, opt => opt.MapFrom(o => o.User.FirstName))
            .ForMember(o => o.UserLastName, opt => opt.MapFrom(o => o.User.LastName))
            .ForMember(o => o.UserTradeName, opt => opt.MapFrom(o => o.User.TradeName))
            .ForMember(o => o.UserCountry, opt => opt.MapFrom(o => o.User.Country))
            .ForMember(o => o.UserCity, opt => opt.MapFrom(o => o.User.City))
            .ForMember(o => o.UserDistrict, opt => opt.MapFrom(o => o.User.District))
            .ForMember(o => o.UserAddress, opt => opt.MapFrom(o => o.User.Address))
            .ForMember(o => o.UserGsmNumber, opt => opt.MapFrom(o => o.User.GsmNumber))
            .ForMember(o => o.UserTaxOffice, opt => opt.MapFrom(o => o.User.TaxOffice))
            .ForMember(o => o.UserTaxNumber, opt => opt.MapFrom(o => o.User.TaxNumber))
            .ForMember(o => o.UserCustomerCode, opt => opt.MapFrom(o => o.User.CustomerCode))
            .ForMember(o => o.UserCarrierCompanyInfo, opt => opt.MapFrom(o => o.User.CarrierCompanyInfo));
        CreateMap<Order, GetListOrderListItemDto>();
        CreateMap<IPaginate<Order>, GetListResponse<GetListOrderListItemDto>>();
    }
}