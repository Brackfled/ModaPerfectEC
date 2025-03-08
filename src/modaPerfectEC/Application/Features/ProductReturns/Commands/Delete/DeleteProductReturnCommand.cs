using Application.Features.ProductReturns.Constants;
using Application.Features.ProductReturns.Constants;
using Application.Features.ProductReturns.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ProductReturns.Constants.ProductReturnsOperationClaims;

namespace Application.Features.ProductReturns.Commands.Delete;

public class DeleteProductReturnCommand : IRequest<DeletedProductReturnResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ProductReturnsOperationClaims.Delete];

    public class DeleteProductReturnCommandHandler : IRequestHandler<DeleteProductReturnCommand, DeletedProductReturnResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductReturnRepository _productReturnRepository;
        private readonly ProductReturnBusinessRules _productReturnBusinessRules;

        public DeleteProductReturnCommandHandler(IMapper mapper, IProductReturnRepository productReturnRepository,
                                         ProductReturnBusinessRules productReturnBusinessRules)
        {
            _mapper = mapper;
            _productReturnRepository = productReturnRepository;
            _productReturnBusinessRules = productReturnBusinessRules;
        }

        public async Task<DeletedProductReturnResponse> Handle(DeleteProductReturnCommand request, CancellationToken cancellationToken)
        {
            ProductReturn? productReturn = await _productReturnRepository.GetAsync(predicate: pr => pr.Id == request.Id, cancellationToken: cancellationToken);
            await _productReturnBusinessRules.ProductReturnShouldExistWhenSelected(productReturn);

            await _productReturnRepository.DeleteAsync(productReturn!);

            DeletedProductReturnResponse response = _mapper.Map<DeletedProductReturnResponse>(productReturn);
            return response;
        }
    }
}