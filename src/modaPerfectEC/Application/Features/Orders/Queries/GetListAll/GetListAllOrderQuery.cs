using Amazon.Runtime.Internal;
using Application.Features.Orders.Constants;
using Application.Services.BasketItems;
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

namespace Application.Features.Orders.Queries.GetListAll;
public class GetListAllOrderQuery: IRequest<ICollection<GetListAllOrderListItemDto>>, ISecuredRequest
{
    public string[] Roles => [Admin, Read];

    public class GetListAllOrderQueryHandler: IRequestHandler<GetListAllOrderQuery, ICollection<GetListAllOrderListItemDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketItemService _basketItemService;
        private IMapper _mapper;

        public GetListAllOrderQueryHandler(IOrderRepository orderRepository, IBasketItemService basketItemService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _basketItemService = basketItemService;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListAllOrderListItemDto>> Handle(GetListAllOrderQuery request, CancellationToken cancellationToken)
        {
            ICollection<Order>? orders = await _orderRepository.GetAllAsync(
                include: opt => opt
                    .Include(o => o.Basket)!
                        .ThenInclude(b => b.BasketItems)!
                            .ThenInclude(bi => bi.Product) // Product için Include
                    .Include(o => o.Basket)
                        .ThenInclude(b => b.BasketItems)!
                            .ThenInclude(bi => bi.ProductVariant)! // ProductVariant için Include
            );

            var response = orders.Select(order => new GetListAllOrderListItemDto
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
                BasketItems = order.Basket?.BasketItems.Where(bi => bi != null).Select(bi => new BasketItemDto
                {
                    Id = bi.Id,
                    ProductName = bi.Product?.Name ?? "Silinmiş Ürün",
                    Color = bi.ProductVariant?.Color ?? "Silinmiş Ürün Renk Varyantı",
                    Hex = bi.ProductVariant?.Hex ?? "Silinmiş Ürün Renk Hex",
                    ProductAmount = bi.ProductAmount,
                    RemainingAfterDelivery = bi.RemainingAfterDelivery,
                }).ToList() ?? new List<BasketItemDto>()
            }).ToList();

            return response;
        }
    }
}
