using Application.Features.Products.Constants;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.Products.Constants.ProductsOperationClaims;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductCommand : IRequest<UpdatedProductResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateProductRequest UpdateProductRequest { get; set; }

    public string[] Roles => [Admin, Write, ProductsOperationClaims.Update];

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdatedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessRules _productBusinessRules;

        public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository,
                                         ProductBusinessRules productBusinessRules)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<UpdatedProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _productBusinessRules.ProductShouldExistWhenSelected(product);
            product = _mapper.Map(request.UpdateProductRequest, product);

            await _productRepository.UpdateAsync(product!);

            UpdatedProductResponse response = _mapper.Map<UpdatedProductResponse>(product);
            return response;
        }
    }
}