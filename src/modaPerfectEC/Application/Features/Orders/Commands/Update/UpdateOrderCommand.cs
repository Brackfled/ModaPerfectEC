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
    public string? OrderNo { get; set; }
    public string? TrackingNumber { get; set; }
    public OrderState? OrderState { get; set; }
    public string[] Roles => [Admin, OrdersOperationClaims.Update];

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

            if(request.OrderNo is not null)
                order!.OrderNo = request.OrderNo;
            if(request.TrackingNumber is not null)
                order!.TrackingNumber = request.TrackingNumber;
            if(request.OrderState is not null)
                order!.OrderState = (OrderState)request.OrderState;

            await _orderRepository.UpdateAsync(order!);

            UpdatedOrderResponse response = _mapper.Map<UpdatedOrderResponse>(order);
            return response;
        }
    }
}