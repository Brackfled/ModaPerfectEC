using Application.Features.ProductVariants.Constants;
using Application.Features.ProductVariants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ProductVariants.Constants.ProductVariantsOperationClaims;

namespace Application.Features.ProductVariants.Commands.Create;

public class CreateProductVariantCommand : IRequest<CreatedProductVariantResponse>, ISecuredRequest, ITransactionalRequest
{
    public required Guid ProductId { get; set; }
    public required string Color { get; set; }
    public required int StockAmount { get; set; }

    public string[] Roles => [Admin, Write, ProductVariantsOperationClaims.Create];

    public class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, CreatedProductVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;

        public CreateProductVariantCommandHandler(IMapper mapper, IProductVariantRepository productVariantRepository,
                                         ProductVariantBusinessRules productVariantBusinessRules)
        {
            _mapper = mapper;
            _productVariantRepository = productVariantRepository;
            _productVariantBusinessRules = productVariantBusinessRules;
        }

        public async Task<CreatedProductVariantResponse> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
        {
            ProductVariant productVariant = _mapper.Map<ProductVariant>(request);

            await _productVariantRepository.AddAsync(productVariant);

            CreatedProductVariantResponse response = _mapper.Map<CreatedProductVariantResponse>(productVariant);
            return response;
        }
    }
}