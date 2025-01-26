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
using Application.Services.ProductVariants;
using Application.Features.ProductVariants.Rules;
using Domain.Dtos;
using Application.Services.ExchangeService;

namespace Application.Features.Products.Commands.Create;

public class CreateProductCommand : IRequest<CreatedProductResponse>, ISecuredRequest, ITransactionalRequest
{
    public CreateProductRequest CreateProductRequest { get; set; }

    public string[] Roles => [Admin, Write, ProductsOperationClaims.Create];

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly IProductVariantService _productVariantService;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;
        private readonly ExchangeServiceBase _exchangeServiceBase;

        public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules, IProductVariantService productVariantService, ProductVariantBusinessRules productVariantBusinessRules, ExchangeServiceBase exchangeServiceBase)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
            _productVariantService = productVariantService;
            _productVariantBusinessRules = productVariantBusinessRules;
            _exchangeServiceBase = exchangeServiceBase;
        }

        public async Task<CreatedProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            await _productBusinessRules.ProductNameShouldNotExistsWhenSelected(request.CreateProductRequest.Name);
            await _productVariantBusinessRules.SizesShouldBeTheRight(request.CreateProductRequest.Sizes);

            double exchangedUSDToTRY = await _exchangeServiceBase.GetUsdExchangeRateAsync();

            double priceUSD = request.CreateProductRequest.Price / exchangedUSDToTRY;
            double flooredPriceUSD = Math.Floor(priceUSD * 100) / 100;

            Product product = new()
            {
                Id = Guid.NewGuid(),
                CategoryId = request.CreateProductRequest.CategoryId,
                SubCategoryId = request.CreateProductRequest.SubCategoryId,
                Name = request.CreateProductRequest.Name,
                Description = request.CreateProductRequest.Description,
                ProductState = request.CreateProductRequest.ProductState,
                Price = request.CreateProductRequest.Price,
                PriceUSD = flooredPriceUSD
            };

            Product addedProduct = await _productRepository.AddAsync(product);

            foreach (ColorDto colorDto in request.CreateProductRequest.ColorDtos)
            {

                await _productVariantBusinessRules.ProductHasAAssociatedProductVariant(addedProduct.Id ,colorDto.Color);
                
                ProductVariant productVariant = new()
                {
                    Id = Guid.NewGuid(),
                    ProductId = addedProduct.Id,
                    Color = colorDto.Color,
                    Hex = colorDto.Hex,
                    Sizes = request.CreateProductRequest.Sizes,
                    StockAmount = colorDto.StockAmount
                };

                await _productVariantService.AddAsync(productVariant);
            }

            CreatedProductResponse response = _mapper.Map<CreatedProductResponse>(addedProduct);
            return response;


        }
    }
}