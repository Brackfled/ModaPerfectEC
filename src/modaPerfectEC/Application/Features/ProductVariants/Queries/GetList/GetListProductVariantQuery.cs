using Application.Features.ProductVariants.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ProductVariants.Constants.ProductVariantsOperationClaims;

namespace Application.Features.ProductVariants.Queries.GetList;

public class GetListProductVariantQuery : IRequest<GetListResponse<GetListProductVariantListItemDto>>//, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    //public string[] Roles => [Admin, Read];

    public class GetListProductVariantQueryHandler : IRequestHandler<GetListProductVariantQuery, GetListResponse<GetListProductVariantListItemDto>>
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IMapper _mapper;

        public GetListProductVariantQueryHandler(IProductVariantRepository productVariantRepository, IMapper mapper)
        {
            _productVariantRepository = productVariantRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProductVariantListItemDto>> Handle(GetListProductVariantQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProductVariant> productVariants = await _productVariantRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListProductVariantListItemDto> response = _mapper.Map<GetListResponse<GetListProductVariantListItemDto>>(productVariants);
            return response;
        }
    }
}