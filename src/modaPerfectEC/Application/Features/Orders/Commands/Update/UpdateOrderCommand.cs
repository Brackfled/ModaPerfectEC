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

namespace Application.Features.Orders.Commands.Update;

public class UpdateOrderCommand : IRequest<UpdatedOrderResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid BasketId { get; set; }
    public required string OrderNo { get; set; }
    public required double OrderPrice { get; set; }
    public string? TrackingNumber { get; set; }
    public required bool IsInvoiceSended { get; set; }
    public required OrderState OrderState { get; set; }

    public string[] Roles => [Admin, Write, OrdersOperationClaims.Update];

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdatedOrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderBusinessRules _orderBusinessRules;

        public UpdateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository,
                                         OrderBusinessRules orderBusinessRules)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<UpdatedOrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order? order = await _orderRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            await _orderBusinessRules.OrderShouldExistWhenSelected(order);
            order = _mapper.Map(request, order);

            await _orderRepository.UpdateAsync(order!);

            UpdatedOrderResponse response = _mapper.Map<UpdatedOrderResponse>(order);
            return response;
        }
    }
}