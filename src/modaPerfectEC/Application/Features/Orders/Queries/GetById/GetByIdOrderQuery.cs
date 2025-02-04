using Application.Features.Orders.Constants;
using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Orders.Constants.OrdersOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.BasketItems;

namespace Application.Features.Orders.Queries.GetById;

public class GetByIdOrderQuery : IRequest<GetByIdOrderResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQuery, GetByIdOrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderBusinessRules _orderBusinessRules;
        private readonly IBasketItemService _basketItemService;

        public GetByIdOrderQueryHandler(IMapper mapper, IOrderRepository orderRepository, OrderBusinessRules orderBusinessRules, IBasketItemService basketItemService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderBusinessRules = orderBusinessRules;
            _basketItemService = basketItemService;
        }

        public async Task<GetByIdOrderResponse> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository
                .Query()
                .Where(o => o.Id == request.Id)
                .Include(o => o.User)
                .Include(o => o.Basket)
                    .ThenInclude(b => b.BasketItems)!
                        .ThenInclude(bi => bi.Product)!
                            .ThenInclude(p => p.ProductImages)
                .Include(o => o.Basket)
                    .ThenInclude(b => b.BasketItems)!
                        .ThenInclude(bi => bi.ProductVariant)
                .FirstOrDefaultAsync(cancellationToken);
            await _orderBusinessRules.OrderShouldExistWhenSelected(order);

            GetByIdOrderResponse response = _mapper.Map<GetByIdOrderResponse>(order);
            return response;
        }
    }
}