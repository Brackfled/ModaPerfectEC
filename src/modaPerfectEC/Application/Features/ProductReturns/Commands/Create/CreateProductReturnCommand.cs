using Application.Features.ProductReturns.Constants;
using Application.Features.ProductReturns.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.ProductReturns.Constants.ProductReturnsOperationClaims;

namespace Application.Features.ProductReturns.Commands.Create;

public class CreateProductReturnCommand : IRequest<CreatedProductReturnResponse>, ISecuredRequest, ITransactionalRequest
{
    public required Guid BasketItemId { get; set; }
    public required Guid OrderId { get; set; }
    public required string Description { get; set; }
    public required ReturnState ReturnState { get; set; }

    public string[] Roles => [Admin, Write, ProductReturnsOperationClaims.Create];

    public class CreateProductReturnCommandHandler : IRequestHandler<CreateProductReturnCommand, CreatedProductReturnResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductReturnRepository _productReturnRepository;
        private readonly ProductReturnBusinessRules _productReturnBusinessRules;

        public CreateProductReturnCommandHandler(IMapper mapper, IProductReturnRepository productReturnRepository,
                                         ProductReturnBusinessRules productReturnBusinessRules)
        {
            _mapper = mapper;
            _productReturnRepository = productReturnRepository;
            _productReturnBusinessRules = productReturnBusinessRules;
        }

        public async Task<CreatedProductReturnResponse> Handle(CreateProductReturnCommand request, CancellationToken cancellationToken)
        {
            ProductReturn productReturn = _mapper.Map<ProductReturn>(request);

            await _productReturnRepository.AddAsync(productReturn);

            CreatedProductReturnResponse response = _mapper.Map<CreatedProductReturnResponse>(productReturn);
            return response;
        }
    }
}