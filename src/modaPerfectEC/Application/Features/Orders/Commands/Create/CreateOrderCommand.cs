using Application.Features.Orders.Constants;
using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.Orders.Constants.OrdersOperationClaims;
using Application.Services.Baskets;
using Application.Features.Baskets.Rules;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<CreatedOrderResponse>, ITransactionalRequest //, ISecuredRequest
{
    public Guid UserId { get; set; }
    public CreateOrderRequest CreateOrderRequest { get; set; }

    //public string[] Roles => [Admin, Write, OrdersOperationClaims.Create];

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreatedOrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderBusinessRules _orderBusinessRules;
        private readonly IBasketService _basketService;
        private readonly BasketBusinessRules _basketBusinessRules;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, OrderBusinessRules orderBusinessRules, IBasketService basketService, BasketBusinessRules basketBusinessRules)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderBusinessRules = orderBusinessRules;
            _basketService = basketService;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<CreatedOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            Basket? basket = await _basketService.GetAsync(
                predicate: b => b.Id == request.CreateOrderRequest.BasketId && b.IsOrderBasket == false && b.UserId == request.UserId,
                include: opt => opt.Include(b => b.User)!.Include(b => b.BasketItems)!,
                cancellationToken: cancellationToken
                );
            
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);
            await _basketBusinessRules.UserShouldHasOneActiveBasket(basket!.UserId);

            Order order = new()
            {
                Id = Guid.NewGuid(),
                UserId = basket.UserId,
                BasketId = basket.Id,
                OrderPrice = request.CreateOrderRequest.OrderPrice,
                IsInvoiceSended = false,
                OrderNo = null,
                IsUsdPrice = request.CreateOrderRequest.IsUsdPrice,
                OrderState = OrderState.Pending,
                TrackingNumber = null
            };

            Order addedOrder = await _orderRepository.AddAsync(order);

            basket.IsOrderBasket = true;
            await _basketService.UpdateAsync(basket);

            Basket newBasket = new()
            {
                Id = Guid.NewGuid(),
                UserId = basket.UserId,
                TotalPrice = 0,
                TotalPriceUSD = 0,
                IsOrderBasket = false
            };

            await _basketService.AddAsync(newBasket);

            CreatedOrderResponse response = _mapper.Map<CreatedOrderResponse>(order);
            return response;
        }
    }
}