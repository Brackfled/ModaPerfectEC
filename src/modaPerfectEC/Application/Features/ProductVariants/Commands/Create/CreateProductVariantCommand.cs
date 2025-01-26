using Application.Features.ProductVariants.Constants;
using Application.Features.ProductVariants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ProductVariants.Constants.ProductVariantsOperationClaims;
using Domain.Dtos;
using Application.Features.Products.Rules;
using Application.Services.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductVariants.Commands.Create;

public class CreateProductVariantCommand : IRequest<CreatedProductVariantResponse>, ISecuredRequest, ITransactionalRequest
{
    public required Guid ProductId { get; set; }
    public required ColorDto ColorDto { get; set; }

    public string[] Roles => [Admin, Write, ProductVariantsOperationClaims.Create];

    public class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, CreatedProductVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly IProductService _productService;

        public CreateProductVariantCommandHandler(IMapper mapper, IProductVariantRepository productVariantRepository, ProductVariantBusinessRules productVariantBusinessRules, ProductBusinessRules productBusinessRules, IProductService productService)
        {
            _mapper = mapper;
            _productVariantRepository = productVariantRepository;
            _productVariantBusinessRules = productVariantBusinessRules;
            _productBusinessRules = productBusinessRules;
            _productService = productService;
        }

        public async Task<CreatedProductVariantResponse> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
        {

            Product? product = await _productService.GetAsync(p => p.Id == request.ProductId, include:opt => opt.Include(p => p.ProductVariants)!);

            await _productBusinessRules.ProductShouldExistWhenSelected(product);
            
            int[] sizes = product!.ProductVariants!.ElementAt(0).Sizes;
            await _productVariantBusinessRules.SizesShouldBeTheRight(sizes);

            ProductVariant productVariant = new()
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Color = request.ColorDto.Color,
                Hex = request.ColorDto.Hex,
                StockAmount = request.ColorDto.StockAmount,
                Sizes = sizes,
            };

            ProductVariant addedProductVariant =  await _productVariantRepository.AddAsync(productVariant);

            CreatedProductVariantResponse response = _mapper.Map<CreatedProductVariantResponse>(addedProductVariant);
            return response;
        }
    }
}