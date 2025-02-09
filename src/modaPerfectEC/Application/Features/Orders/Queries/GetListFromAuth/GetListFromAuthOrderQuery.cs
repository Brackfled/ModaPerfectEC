using Amazon.Runtime.Internal;
using Application.Features.Orders.Constants;
using Application.Features.Orders.Queries.GetListAll;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Orders.Constants.OrdersOperationClaims;


namespace Application.Features.Orders.Queries.GetListFromAuth;
public class GetListFromAuthOrderQuery: IRequest<ICollection<GetListFromAuthOrderListItemDto>>, ISecuredRequest
{
    public Guid UserId { get; set; }

    public string[] Roles => [Admin, OrdersOperationClaims.GetListFromAuth];

    public class GetListFromAuthOrderQueryHandler: IRequestHandler<GetListFromAuthOrderQuery, ICollection<GetListFromAuthOrderListItemDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private IMapper _mapper;

        public GetListFromAuthOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListFromAuthOrderListItemDto>> Handle(GetListFromAuthOrderQuery request, CancellationToken cancellationToken)
        {
            ICollection<Order>? orders = await _orderRepository.GetAllAsync(
                predicate:o => o.UserId == request.UserId,
                include: opt => opt
                    .Include(o => o.Basket)!
                        .ThenInclude(b => b.BasketItems)!
                            .ThenInclude(bi => bi.Product) // Product için Include
                    .Include(o => o.Basket)
                        .ThenInclude(b => b.BasketItems)!
                            .ThenInclude(bi => bi.ProductVariant)! // ProductVariant için Include
            );

            var response = orders.Select(order => new GetListFromAuthOrderListItemDto
            {
                Id = order.Id,
                OrderNo = order.OrderNo,
                OrderPrice = order.OrderPrice,
                TrackingNumber = order.TrackingNumber,
                IsInvoiceSended = order.IsInvoiceSended,
                IsUsdPrice = order.IsUsdPrice,
                OrderState = order.OrderState,
                CreatedDate = order.CreatedDate,
                UpdatedDate = order.UpdatedDate,
                DeleteDDate = order.DeletedDate,
                BasketItems = order.Basket?.BasketItems.Select(bi => new BasketItemDto
                {
                    Id = bi.Id,
                    ProductName = bi.Product!.Name, // Ürün adı
                    Color = bi.ProductVariant!.Color, // Eğer ProductVariant tek nesne ise direkt adını al
                    Hex = bi.ProductVariant!.Hex,
                    ProductAmount = bi.ProductAmount,
                    RemainingAfterDelivery = bi.RemainingAfterDelivery,
                }).ToList() ?? new List<BasketItemDto>()
            }).ToList();

            return response;
        }
    }
}
