using Application.Features.ProductVariants.Constants;
using Application.Features.ProductVariants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ProductVariants.Constants.ProductVariantsOperationClaims;

namespace Application.Features.ProductVariants.Commands.Update;

public class UpdateProductVariantCommand : IRequest<UpdatedProductVariantResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid ProductId { get; set; }
    public required string Color { get; set; }
    public required int StockAmount { get; set; }

    public string[] Roles => [Admin, Write, ProductVariantsOperationClaims.Update];

    public class UpdateProductVariantCommandHandler : IRequestHandler<UpdateProductVariantCommand, UpdatedProductVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;

        public UpdateProductVariantCommandHandler(IMapper mapper, IProductVariantRepository productVariantRepository,
                                         ProductVariantBusinessRules productVariantBusinessRules)
        {
            _mapper = mapper;
            _productVariantRepository = productVariantRepository;
            _productVariantBusinessRules = productVariantBusinessRules;
        }

        public async Task<UpdatedProductVariantResponse> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
        {
            ProductVariant? productVariant = await _productVariantRepository.GetAsync(predicate: pv => pv.Id == request.Id, cancellationToken: cancellationToken);
            await _productVariantBusinessRules.ProductVariantShouldExistWhenSelected(productVariant);
            productVariant = _mapper.Map(request, productVariant);

            await _productVariantRepository.UpdateAsync(productVariant!);

            UpdatedProductVariantResponse response = _mapper.Map<UpdatedProductVariantResponse>(productVariant);
            return response;
        }
    }
}