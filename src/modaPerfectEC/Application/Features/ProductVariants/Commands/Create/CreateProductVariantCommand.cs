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

namespace Application.Features.ProductVariants.Commands.Create;

public class CreateProductVariantCommand : IRequest<CreatedProductVariantResponse>, ISecuredRequest, ITransactionalRequest
{
    public required Guid ProductId { get; set; }
    public required ColorDto ColorDto { get; set; }
    public required int[] Sizes { get; set; }

    public string[] Roles => [Admin, Write, ProductVariantsOperationClaims.Create];

    public class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, CreatedProductVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;
        private readonly ProductBusinessRules _productBusinessRules;

        public CreateProductVariantCommandHandler(IMapper mapper, IProductVariantRepository productVariantRepository, ProductVariantBusinessRules productVariantBusinessRules, ProductBusinessRules productBusinessRules)
        {
            _mapper = mapper;
            _productVariantRepository = productVariantRepository;
            _productVariantBusinessRules = productVariantBusinessRules;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<CreatedProductVariantResponse> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductIdShouldExistWhenSelected(request.ProductId, cancellationToken);
            await _productVariantBusinessRules.SizesShouldBeTheRight(request.Sizes);

            ProductVariant productVariant = new()
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Color = request.ColorDto.Color,
                Hex = request.ColorDto.Hex,
                StockAmount = request.ColorDto.StockAmount,
                Sizes = request.Sizes,
            };

            ProductVariant addedProductVariant =  await _productVariantRepository.AddAsync(productVariant);

            CreatedProductVariantResponse response = _mapper.Map<CreatedProductVariantResponse>(addedProductVariant);
            return response;
        }
    }
}