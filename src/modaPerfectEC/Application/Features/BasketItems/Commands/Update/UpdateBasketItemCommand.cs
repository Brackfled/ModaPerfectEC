using Application.Features.BasketItems.Constants;
using Application.Features.BasketItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.BasketItems.Constants.BasketItemsOperationClaims;

namespace Application.Features.BasketItems.Commands.Update;

public class UpdateBasketItemCommand : IRequest<UpdatedBasketItemResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid BasketId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? ProductVariantId { get; set; }
    public required int ProductAmount { get; set; }
    public required int RemainingAfterDelivery { get; set; }
    public required bool IsReturned { get; set; }

    public string[] Roles => [Admin, Write, BasketItemsOperationClaims.Update];

    public class UpdateBasketItemCommandHandler : IRequestHandler<UpdateBasketItemCommand, UpdatedBasketItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;

        public UpdateBasketItemCommandHandler(IMapper mapper, IBasketItemRepository basketItemRepository,
                                         BasketItemBusinessRules basketItemBusinessRules)
        {
            _mapper = mapper;
            _basketItemRepository = basketItemRepository;
            _basketItemBusinessRules = basketItemBusinessRules;
        }

        public async Task<UpdatedBasketItemResponse> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            BasketItem? basketItem = await _basketItemRepository.GetAsync(predicate: bi => bi.Id == request.Id, cancellationToken: cancellationToken);
            await _basketItemBusinessRules.BasketItemShouldExistWhenSelected(basketItem);
            basketItem = _mapper.Map(request, basketItem);

            await _basketItemRepository.UpdateAsync(basketItem!);

            UpdatedBasketItemResponse response = _mapper.Map<UpdatedBasketItemResponse>(basketItem);
            return response;
        }
    }
}