﻿using Amazon.Runtime.Internal;
using Application.Services.BasketItems;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetListAll;
public class GetListAllOrderQuery: IRequest<ICollection<GetListAllOrderListItemDto>>
{
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
