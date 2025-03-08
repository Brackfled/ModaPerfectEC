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

namespace Application.Features.ProductReturns.Commands.Update;

public class UpdateProductReturnCommand : IRequest<UpdatedProductReturnResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid BasketItemId { get; set; }
    public required Guid OrderId { get; set; }
    public required string Description { get; set; }
    public required ReturnState ReturnState { get; set; }

    public string[] Roles => [Admin, Write, ProductReturnsOperationClaims.Update];

    public class UpdateProductReturnCommandHandler : IRequestHandler<UpdateProductReturnCommand, UpdatedProductReturnResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductReturnRepository _productReturnRepository;
        private readonly ProductReturnBusinessRules _productReturnBusinessRules;

        public UpdateProductReturnCommandHandler(IMapper mapper, IProductReturnRepository productReturnRepository,
                                         ProductReturnBusinessRules productReturnBusinessRules)
        {
            _mapper = mapper;
            _productReturnRepository = productReturnRepository;
            _productReturnBusinessRules = productReturnBusinessRules;
        }

        public async Task<UpdatedProductReturnResponse> Handle(UpdateProductReturnCommand request, CancellationToken cancellationToken)
        {
            ProductReturn? productReturn = await _productReturnRepository.GetAsync(predicate: pr => pr.Id == request.Id, cancellationToken: cancellationToken);
            await _productReturnBusinessRules.ProductReturnShouldExistWhenSelected(productReturn);
            productReturn = _mapper.Map(request, productReturn);

            await _productReturnRepository.UpdateAsync(productReturn!);

            UpdatedProductReturnResponse response = _mapper.Map<UpdatedProductReturnResponse>(productReturn);
            return response;
        }
    }
}