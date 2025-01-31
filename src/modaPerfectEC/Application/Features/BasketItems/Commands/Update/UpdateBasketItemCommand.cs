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
using Application.Features.ProductVariants.Rules;

namespace Application.Features.BasketItems.Commands.Update;

public class UpdateBasketItemCommand : IRequest<UpdatedBasketItemResponse>, ITransactionalRequest //,ISecuredRequest
{
    public Guid Id { get; set; }
    public int ProcessAmount { get; set; }
    public bool Increase {  get; set; }

    //public string[] Roles => [Admin, Write, BasketItemsOperationClaims.Update];

    public class UpdateBasketItemCommandHandler : IRequestHandler<UpdateBasketItemCommand, UpdatedBasketItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IBasketService _basketService;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;
        private readonly BasketBusinessRules _basketBusinessRules;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;

        public UpdateBasketItemCommandHandler(IMapper mapper, IBasketItemRepository basketItemRepository, IBasketService basketService, BasketItemBusinessRules basketItemBusinessRules, BasketBusinessRules basketBusinessRules, ProductVariantBusinessRules productVariantBusinessRules)
        {
            _mapper = mapper;
            _basketItemRepository = basketItemRepository;
            _basketService = basketService;
            _basketItemBusinessRules = basketItemBusinessRules;
            _basketBusinessRules = basketBusinessRules;
            _productVariantBusinessRules = productVariantBusinessRules;
        }

        public async Task<UpdatedBasketItemResponse> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
        {
            BasketItem? basketItem = await _basketItemRepository.GetAsync(
                predicate: bi => bi.Id == request.Id, 
                include:opt => opt.Include(bi => bi.Product)!.Include(p => p.ProductVariant)! ,
                cancellationToken: cancellationToken);
            await _basketItemBusinessRules.BasketItemShouldExistWhenSelected(basketItem);

            Basket? basket = await _basketService.GetAsync(b => b.Id == basketItem!.BasketId);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

            if (request.Increase)
            {
                await _productVariantBusinessRules.StockAmountIsAvailabla(basketItem!.ProductVariant!.Id, request.ProcessAmount);

                basketItem!.ProductAmount += request.ProcessAmount;
                basket!.TotalPrice = Math.Round(basket.TotalPrice + ((request.ProcessAmount * basketItem.Product!.Price) * basketItem.ProductVariant.Sizes.Length), 2);
                basket!.TotalPriceUSD = Math.Round(basket.TotalPriceUSD+ ((request.ProcessAmount * basketItem.Product!.Price) * basketItem.ProductVariant.Sizes.Length), 2);

            }

            if (!request.Increase)
            {
                basketItem!.ProductAmount -= request.ProcessAmount;
                basket!.TotalPrice = Math.Round(basket.TotalPrice - ((request.ProcessAmount * basketItem.Product!.Price) * basketItem.ProductVariant!.Sizes.Length), 2);
                basket!.TotalPriceUSD= Math.Round(basket.TotalPriceUSD- ((request.ProcessAmount * basketItem.Product!.PriceUSD) * basketItem.ProductVariant.Sizes.Length), 2);
            }

            await _basketItemRepository.UpdateAsync(basketItem!);
            await _basketService.UpdateAsync(basket!);

            UpdatedBasketItemResponse response = _mapper.Map<UpdatedBasketItemResponse>(basketItem);
            return response;
        }
    }
}