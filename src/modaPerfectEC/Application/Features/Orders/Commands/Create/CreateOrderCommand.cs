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

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<CreatedOrderResponse>, ISecuredRequest, ITransactionalRequest
{
    public required Guid UserId { get; set; }
    public required Guid BasketId { get; set; }
    public required string OrderNo { get; set; }
    public required double OrderPrice { get; set; }
    public string? TrackingNumber { get; set; }
    public required bool IsInvoiceSended { get; set; }
    public required OrderState OrderState { get; set; }

    public string[] Roles => [Admin, Write, OrdersOperationClaims.Create];

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreatedOrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderBusinessRules _orderBusinessRules;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository,
                                         OrderBusinessRules orderBusinessRules)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<CreatedOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = _mapper.Map<Order>(request);

            await _orderRepository.AddAsync(order);

            CreatedOrderResponse response = _mapper.Map<CreatedOrderResponse>(order);
            return response;
        }
    }
}