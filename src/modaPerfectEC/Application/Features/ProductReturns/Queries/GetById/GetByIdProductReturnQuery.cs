using Application.Features.ProductReturns.Constants;
using Application.Features.ProductReturns.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ProductReturns.Constants.ProductReturnsOperationClaims;

namespace Application.Features.ProductReturns.Queries.GetById;

public class GetByIdProductReturnQuery : IRequest<GetByIdProductReturnResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdProductReturnQueryHandler : IRequestHandler<GetByIdProductReturnQuery, GetByIdProductReturnResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductReturnRepository _productReturnRepository;
        private readonly ProductReturnBusinessRules _productReturnBusinessRules;

        public GetByIdProductReturnQueryHandler(IMapper mapper, IProductReturnRepository productReturnRepository, ProductReturnBusinessRules productReturnBusinessRules)
        {
            _mapper = mapper;
            _productReturnRepository = productReturnRepository;
            _productReturnBusinessRules = productReturnBusinessRules;
        }

        public async Task<GetByIdProductReturnResponse> Handle(GetByIdProductReturnQuery request, CancellationToken cancellationToken)
        {
            ProductReturn? productReturn = await _productReturnRepository.GetAsync(predicate: pr => pr.Id == request.Id, cancellationToken: cancellationToken);
            await _productReturnBusinessRules.ProductReturnShouldExistWhenSelected(productReturn);

            GetByIdProductReturnResponse response = _mapper.Map<GetByIdProductReturnResponse>(productReturn);
            return response;
        }
    }
}