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

public class CreateBasketItemCommand : IRequest<CreatedBasketItemResponse>, ITransactionalRequest //, ISecuredRequest
{
    public required Guid UserId { get; set; }
    public CreateBasketItemRequest CreateBasketItemRequest { get; set; }

    //public string[] Roles => [Admin, Write, BasketItemsOperationClaims.Create];

    public class CreateBasketItemCommandHandler : IRequestHandler<CreateBasketItemCommand, CreatedBasketItemResponse>
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

        public async Task<CreatedBasketItemResponse> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            await _productVariantBusinessRules.StockAmountIsAvailabla(request.CreateBasketItemRequest.ProductVariantId, request.CreateBasketItemRequest.ProductAmount);

            CreatedBasketItemResponse response;

            await _basketBusinessRules.UserShouldHasOneActiveBasket(request.UserId);

            Basket? basket = await _basketService.GetAsync(b => b.UserId == request.UserId && b.IsOrderBasket == false, include:opt => opt.Include(b => b.BasketItems)!);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

            BasketItem? basketItem = await _basketItemRepository.GetAsync(
                    predicate:bi => bi.ProductVariantId == request.CreateBasketItemRequest.ProductVariantId,
                    include:(opt=> opt.Include(bi => bi.Product)!),
                    cancellationToken:cancellationToken
                );

            if (basketItem is not null)
            {
                basketItem.ProductAmount += request.CreateBasketItemRequest.ProductAmount;
                basket!.TotalPrice = Math.Round(basket.TotalPrice + (request.CreateBasketItemRequest.ProductAmount * basketItem.Product!.Price), 2);

                await _basketItemRepository.UpdateAsync(basketItem);
                await _basketService.UpdateAsync(basket);

                response = _mapper.Map<CreatedBasketItemResponse>(basketItem);
            }
            else
            {
                BasketItem addedBasketItem = await _basketItemRepository.AddAsync(
                        new()
                        {
                            Id = Guid.NewGuid(),
                            ProductId = request.CreateBasketItemRequest.ProductId,
                            ProductVariantId = request.CreateBasketItemRequest.ProductVariantId,
                            ProductAmount = request.CreateBasketItemRequest.ProductAmount,
                            IsReturned = false,
                            BasketId = basket!.Id,
                            RemainingAfterDelivery = 0
                        }
                    );

                Product? product = await _productService.GetAsync(p => p.Id == request.CreateBasketItemRequest.ProductId);
                await _productBusinessRules.ProductShouldExistWhenSelected(product);

                basket!.TotalPrice = Math.Round(basket.TotalPrice + (request.CreateBasketItemRequest.ProductAmount * product!.Price), 2, MidpointRounding.AwayFromZero);
                await _basketService.UpdateAsync(basket);

                response = _mapper.Map<CreatedBasketItemResponse>(addedBasketItem);
            }



            return response;
        }
    }
}