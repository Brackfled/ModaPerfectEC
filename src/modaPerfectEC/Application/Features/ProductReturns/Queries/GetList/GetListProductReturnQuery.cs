using Application.Features.ProductReturns.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ProductReturns.Constants.ProductReturnsOperationClaims;

namespace Application.Features.ProductReturns.Queries.GetList;

public class GetListProductReturnQuery : IRequest<GetListResponse<GetListProductReturnListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListProductReturnQueryHandler : IRequestHandler<GetListProductReturnQuery, GetListResponse<GetListProductReturnListItemDto>>
    {
        private readonly IProductReturnRepository _productReturnRepository;
        private readonly IMapper _mapper;

        public GetListProductReturnQueryHandler(IProductReturnRepository productReturnRepository, IMapper mapper)
        {
            _productReturnRepository = productReturnRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProductReturnListItemDto>> Handle(GetListProductReturnQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProductReturn> productReturns = await _productReturnRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListProductReturnListItemDto> response = _mapper.Map<GetListResponse<GetListProductReturnListItemDto>>(productReturns);
            return response;
        }
    }
}