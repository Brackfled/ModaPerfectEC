using Application.Features.BasketItems.Constants;
using Application.Features.BasketItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.BasketItems.Constants.BasketItemsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.Baskets;
using Application.Features.Baskets.Rules;

namespace Application.Features.BasketItems.Commands.Delete;

public class DeleteBasketItemCommand : IRequest<DeletedBasketItemResponse>, ITransactionalRequest //,ISecuredRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }

    //public string[] Roles => [Admin, Write, BasketItemsOperationClaims.Delete];

    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommand, DeletedBasketItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;
        private readonly IBasketService _basketService;
        private readonly BasketBusinessRules _basketBusinessRules;

        public DeleteBasketItemCommandHandler(IMapper mapper, IBasketItemRepository basketItemRepository, BasketItemBusinessRules basketItemBusinessRules, IBasketService basketService, BasketBusinessRules basketBusinessRules)
        {
            _mapper = mapper;
            _basketItemRepository = basketItemRepository;
            _basketItemBusinessRules = basketItemBusinessRules;
            _basketService = basketService;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<DeletedBasketItemResponse> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            BasketItem? basketItem = await _basketItemRepository.GetAsync(predicate: bi => bi.Id == request.Id, include:opt => opt.Include(bi => bi.Product)!, cancellationToken: cancellationToken);
            await _basketItemBusinessRules.BasketItemShouldExistWhenSelected(basketItem);

            Basket? basket = await _basketService.GetAsync(b => b.Id == basketItem!.BasketId);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

            basket!.TotalPrice = Math.Round(basket.TotalPrice - (basketItem!.ProductAmount * basketItem!.Product!.Price), 2, MidpointRounding.AwayFromZero);
            await _basketService.UpdateAsync(basket);

            await _basketItemRepository.DeleteAsync(basketItem!, true);

            DeletedBasketItemResponse response = _mapper.Map<DeletedBasketItemResponse>(basketItem);
            return response;
        }
    }
}