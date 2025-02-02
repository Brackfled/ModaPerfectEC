using Application.Features.BasketItems.Constants;
using Application.Features.BasketItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.BasketItems.Constants.BasketItemsOperationClaims;
using Application.Services.Baskets;
using Application.Features.Baskets.Rules;
using Microsoft.EntityFrameworkCore;
using Application.Services.Products;
using Application.Features.Products.Rules;
using Application.Features.ProductVariants.Rules;

namespace Application.Features.BasketItems.Commands.Create;

public class CreateBasketItemCommand : IRequest<ICollection<CreatedBasketItemResponse>>, ITransactionalRequest , ISecuredRequest
{
    public required Guid UserId { get; set; }
    public IList<CreateBasketItemRequest> CreateBasketItemRequests { get; set; }

    public string[] Roles => [Admin, BasketItemsOperationClaims.Create];

    public class CreateBasketItemCommandHandler : IRequestHandler<CreateBasketItemCommand, ICollection<CreatedBasketItemResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;
        private readonly IBasketService _basketService;
        private readonly BasketBusinessRules _basketBusinessRules;
        private readonly IProductService _productService;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;

        public CreateBasketItemCommandHandler(IMapper mapper, IBasketItemRepository basketItemRepository, BasketItemBusinessRules basketItemBusinessRules, IBasketService basketService, BasketBusinessRules basketBusinessRules, IProductService productService, ProductBusinessRules productBusinessRules, ProductVariantBusinessRules productVariantBusinessRules)
        {
            _mapper = mapper;
            _basketItemRepository = basketItemRepository;
            _basketItemBusinessRules = basketItemBusinessRules;
            _basketService = basketService;
            _basketBusinessRules = basketBusinessRules;
            _productService = productService;
            _productBusinessRules = productBusinessRules;
            _productVariantBusinessRules = productVariantBusinessRules;
        }

        public async Task<ICollection<CreatedBasketItemResponse>> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {

            ICollection<BasketItem> basketItems = new List<BasketItem>();

            await _basketBusinessRules.UserShouldHasOneActiveBasket(request.UserId);

            Basket? basket = await _basketService.GetAsync(b => b.UserId == request.UserId && b.IsOrderBasket == false, include:opt => opt.Include(b => b.BasketItems)!);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

            foreach(CreateBasketItemRequest rq in request.CreateBasketItemRequests)
            {
                await _productVariantBusinessRules.StockAmountIsAvailabla(rq.ProductVariantId, rq.ProductAmount);
                await _basketItemBusinessRules.ProductAmountGreatherThenZero(rq.ProductAmount);


                BasketItem? basketItem = await _basketItemRepository.GetAsync(
                    predicate: bi => bi.BasketId == basket!.Id && bi.ProductVariantId == rq.ProductVariantId,
                    include: (opt => opt.Include(bi => bi.Product)!.Include(bi => bi.ProductVariant)!),
                    cancellationToken: cancellationToken
                );

                if (basketItem is not null)
                {
                    basketItem.ProductAmount += rq.ProductAmount;
                    basket!.TotalPrice = Math.Round(basket.TotalPrice + ((rq.ProductAmount * basketItem.Product!.Price) * basketItem!.ProductVariant!.Sizes.Length), 2);
                    basket!.TotalPriceUSD = Math.Round(basket.TotalPriceUSD + ((rq.ProductAmount * basketItem.Product!.PriceUSD) * basketItem.ProductVariant.Sizes.Length), 2);

                    await _basketItemRepository.UpdateAsync(basketItem);
                    await _basketService.UpdateAsync(basket);

                    basketItems.Add(basketItem);
                }
                else
                {
                    Product? product = await _productService.GetAsync(p => p.Id == rq.ProductId);
                    await _productBusinessRules.ProductShouldExistWhenSelected(product);
                    await _productBusinessRules.ProductShouldBeActive(product);

                    BasketItem addedBasketItem = await _basketItemRepository.AddAsync(
                            new()
                            {
                                Id = Guid.NewGuid(),
                                ProductId = rq.ProductId,
                                ProductVariantId = rq.ProductVariantId,
                                ProductAmount = rq.ProductAmount,
                                IsReturned = false,
                                BasketId = basket!.Id,
                                RemainingAfterDelivery = 0
                            }
                        );

                    basket!.TotalPrice = Math.Round(basket.TotalPrice + ((rq.ProductAmount * product!.Price) * addedBasketItem!.ProductVariant!.Sizes.Length), 2, MidpointRounding.AwayFromZero);
                    basket!.TotalPriceUSD = Math.Round(basket.TotalPriceUSD + ((rq.ProductAmount * product!.PriceUSD) * addedBasketItem!.ProductVariant!.Sizes.Length), 2, MidpointRounding.AwayFromZero);
                    await _basketService.UpdateAsync(basket);

                    basketItems.Add(addedBasketItem);
                }
            }
            ICollection<CreatedBasketItemResponse> response = _mapper.Map<ICollection<CreatedBasketItemResponse>>(basketItems);
            return response;
        }
    }
}