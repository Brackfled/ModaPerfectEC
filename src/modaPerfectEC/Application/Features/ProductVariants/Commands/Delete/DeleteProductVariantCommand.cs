using Application.Features.ProductVariants.Constants;
using Application.Features.ProductVariants.Constants;
using Application.Features.ProductVariants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ProductVariants.Constants.ProductVariantsOperationClaims;

namespace Application.Features.ProductVariants.Commands.Delete;

public class DeleteProductVariantCommand : IRequest<DeletedProductVariantResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ProductVariantsOperationClaims.Delete];

    public class DeleteProductVariantCommandHandler : IRequestHandler<DeleteProductVariantCommand, DeletedProductVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;

        public DeleteProductVariantCommandHandler(IMapper mapper, IProductVariantRepository productVariantRepository,
                                         ProductVariantBusinessRules productVariantBusinessRules)
        {
            _mapper = mapper;
            _productVariantRepository = productVariantRepository;
            _productVariantBusinessRules = productVariantBusinessRules;
        }

        public async Task<DeletedProductVariantResponse> Handle(DeleteProductVariantCommand request, CancellationToken cancellationToken)
        {
            ProductVariant? productVariant = await _productVariantRepository.GetAsync(predicate: pv => pv.Id == request.Id, cancellationToken: cancellationToken);
            await _productVariantBusinessRules.ProductVariantShouldExistWhenSelected(productVariant);

            await _productVariantRepository.DeleteAsync(productVariant!, true);

            DeletedProductVariantResponse response = _mapper.Map<DeletedProductVariantResponse>(productVariant);
            return response;
        }
    }
}