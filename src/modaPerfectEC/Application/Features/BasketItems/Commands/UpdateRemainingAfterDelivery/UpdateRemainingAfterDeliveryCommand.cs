using Amazon.Runtime.Internal;
using Application.Features.BasketItems.Constants;
using Application.Features.BasketItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.BasketItems.Constants.BasketItemsOperationClaims;

namespace Application.Features.BasketItems.Commands.UpdateRemainingAfterDelivery;
public class UpdateRemainingAfterDeliveryCommand: IRequest<UpdatedRemainingAfterDeliveryResponse>, ITransactionalRequest, ISecuredRequest
{
    public Guid Id { get; set; }
    public int DeliveredAmount { get; set; }

    public string[] Roles => [Admin, BasketItemsOperationClaims.Update];

    public class UpdateRemainingAfterDeliveryCommandHandler: IRequestHandler<UpdateRemainingAfterDeliveryCommand, UpdatedRemainingAfterDeliveryResponse>
    {
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;
        private IMapper _mapper;

        public UpdateRemainingAfterDeliveryCommandHandler(IBasketItemRepository basketItemRepository, BasketItemBusinessRules basketItemBusinessRules, IMapper mapper)
        {
            _basketItemRepository = basketItemRepository;
            _basketItemBusinessRules = basketItemBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdatedRemainingAfterDeliveryResponse> Handle(UpdateRemainingAfterDeliveryCommand request, CancellationToken cancellationToken)
        {
            BasketItem? basketItem = await _basketItemRepository.GetAsync(bi => bi.Id == request.Id);

            await _basketItemBusinessRules.BasketItemShouldExistWhenSelected(basketItem);
            await _basketItemBusinessRules.DeliveredAmountIsCorrect(basketItem!, request.DeliveredAmount);

            basketItem!.RemainingAfterDelivery = basketItem.RemainingAfterDelivery + request.DeliveredAmount;

            BasketItem updatedBasketItem = await _basketItemRepository.UpdateAsync(basketItem);
            UpdatedRemainingAfterDeliveryResponse response = _mapper.Map<UpdatedRemainingAfterDeliveryResponse>(updatedBasketItem);
            return response;

        }
    }
}
