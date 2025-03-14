using Application.Features.Products.Constants;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.Products.Constants.ProductsOperationClaims;
using Application.Services.ExchangeService;
using Application.Services.BasketItems;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductCommand : IRequest<UpdatedProductResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateProductRequest UpdateProductRequest { get; set; }

    public string[] Roles => [Admin, ProductsOperationClaims.Update];

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdatedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly ExchangeServiceBase _exchangeServiceBase;
        private readonly IBasketItemService _basketItemService;

        public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules, ExchangeServiceBase exchangeServiceBase, IBasketItemService basketItemService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
            _exchangeServiceBase = exchangeServiceBase;
            _basketItemService = basketItemService;
        }

        public async Task<UpdatedProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _productBusinessRules.ProductShouldExistWhenSelected(product);

            if (request.UpdateProductRequest.Price is not null && request.UpdateProductRequest.Price != product!.Price)
            {
                double exchangedUSDToTRY = await _exchangeServiceBase.GetUsdExchangeRateAsync();
                double priceUSD = (double)(request.UpdateProductRequest.Price / exchangedUSDToTRY);
                double flooredPriceUSD = Math.Floor(priceUSD * 100) / 100;

                product.PriceUSD = flooredPriceUSD;

                await _basketItemService.DeleteAllByProductIdAsync(product.Id);
            }

            if ((product!.ProductState == ProductState.Active || product.ProductState == ProductState.Showcase) && request.UpdateProductRequest.ProductState == ProductState.Passive)
                await _basketItemService.DeleteAllByProductIdAsync(product.Id);

            product = _mapper.Map(request.UpdateProductRequest, product);

            await _productRepository.UpdateAsync(product!);

            UpdatedProductResponse response = _mapper.Map<UpdatedProductResponse>(product);
            return response;
        }
    }
}