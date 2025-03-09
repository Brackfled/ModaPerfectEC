using Application.Features.Products.Constants;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Products.Constants.ProductsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.BasketItems;
using Application.Features.BasketItems.Rules;
using Application.Services.Baskets;
using Application.Features.Baskets.Rules;

namespace Application.Features.Products.Commands.Delete;

public class DeleteProductCommand : IRequest<DeletedProductResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, ProductsOperationClaims.Delete];

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeletedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly IBasketItemService _basketItemService;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;
        private readonly IBasketService _basketService;
        private readonly BasketBusinessRules _basketBusinessRules;

        public DeleteProductCommandHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules, IBasketItemService basketItemService, BasketItemBusinessRules basketItemBusinessRules, IBasketService basketService, BasketBusinessRules basketBusinessRules)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
            _basketItemService = basketItemService;
            _basketItemBusinessRules = basketItemBusinessRules;
            _basketService = basketService;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<DeletedProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetAsync(
                predicate: p => p.Id == request.Id,
                include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!,
                cancellationToken: cancellationToken);
            await _productBusinessRules.ProductShouldExistWhenSelected(product);

            // Use AsNoTracking for read-only basket items query
            var basketItems = await _basketItemService.GetAllAsync(
                bi => bi.ProductId == product!.Id,
                include: opt => opt.Include(bi => bi.ProductVariant!));

            foreach (var bItem in basketItems)
            {
                // Get the basket without tracking
                var basket = await _basketService.GetAsync(
                    b => b.Id == bItem.BasketId);
                await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

                // Create a new basket item instance for update
                var basketItemToUpdate = new BasketItem
                {
                    Id = bItem.Id,
                    BasketId = bItem.BasketId,
                    ProductId = null,
                    ProductVariantId = null,
                    ProductAmount = bItem.ProductAmount
                };

                await _basketItemService.UpdateAsync(basketItemToUpdate);

                if (!basket!.IsOrderBasket)
                {
                    var basketToUpdate = new Basket
                    {
                        Id = basket.Id,
                        TotalPrice = Math.Round(basket.TotalPrice - ((bItem.ProductAmount * product!.Price) * bItem.ProductVariant!.Sizes.Length), 2, MidpointRounding.AwayFromZero),
                        TotalPriceUSD = Math.Round(basket.TotalPriceUSD - ((bItem.ProductAmount * product!.PriceUSD) * bItem.ProductVariant.Sizes.Length), 2, MidpointRounding.AwayFromZero),
                        IsOrderBasket = basket.IsOrderBasket
                    };

                    await _basketService.UpdateAsync(basketToUpdate);
                }
            }

            await _productRepository.DeleteAsync(product!, true);

            DeletedProductResponse response = _mapper.Map<DeletedProductResponse>(product);
            return response;
        }
    }
}