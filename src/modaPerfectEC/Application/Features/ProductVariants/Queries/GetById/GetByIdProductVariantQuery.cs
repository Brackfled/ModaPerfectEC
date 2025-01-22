using Application.Features.ProductVariants.Constants;
using Application.Features.ProductVariants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ProductVariants.Constants.ProductVariantsOperationClaims;

namespace Application.Features.ProductVariants.Queries.GetById;

public class GetByIdProductVariantQuery : IRequest<GetByIdProductVariantResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    //public string[] Roles => [Admin, Read];

    public class GetByIdProductVariantQueryHandler : IRequestHandler<GetByIdProductVariantQuery, GetByIdProductVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;

        public GetByIdProductVariantQueryHandler(IMapper mapper, IProductVariantRepository productVariantRepository, ProductVariantBusinessRules productVariantBusinessRules)
        {
            _mapper = mapper;
            _productVariantRepository = productVariantRepository;
            _productVariantBusinessRules = productVariantBusinessRules;
        }

        public async Task<GetByIdProductVariantResponse> Handle(GetByIdProductVariantQuery request, CancellationToken cancellationToken)
        {
            ProductVariant? productVariant = await _productVariantRepository.GetAsync(predicate: pv => pv.Id == request.Id, cancellationToken: cancellationToken);
            await _productVariantBusinessRules.ProductVariantShouldExistWhenSelected(productVariant);

            GetByIdProductVariantResponse response = _mapper.Map<GetByIdProductVariantResponse>(productVariant);
            return response;
        }
    }
}