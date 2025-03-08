using Application.Features.Products.Constants;
using Application.Features.Products.Constants;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Products.Constants.ProductsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.BasketItems;
using Application.Features.BasketItems.Rules;

namespace Application.Features.Products.Commands.Delete;

public class DeleteProductCommand : IRequest<DeletedProductResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, ProductsOperationClaims.Delete];

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeletedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly IBasketItemService _basketItemService;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;

        public DeleteProductCommandHandler(IMapper mapper, IProductRepository productRepository, ProductBusinessRules productBusinessRules, IBasketItemService basketItemService, BasketItemBusinessRules basketItemBusinessRules)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
            _basketItemService = basketItemService;
            _basketItemBusinessRules = basketItemBusinessRules;
        }

        public async Task<DeletedProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetAsync(
                predicate: p => p.Id == request.Id,
                include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!,
                cancellationToken: cancellationToken);
            await _productBusinessRules.ProductShouldExistWhenSelected(product);

            ICollection<BasketItem> basketItems = await _basketItemService.GetAllAsync(bi => bi.ProductId == product!.Id);

            foreach (BasketItem bItem in basketItems)
            {
                bItem.ProductId = null;
                bItem.ProductVariantId = null;

                await _basketItemService.UpdateAsync(bItem);
            }

            await _productRepository.DeleteAsync(product!, true);

            DeletedProductResponse response = _mapper.Map<DeletedProductResponse>(product);
            return response;
        }
    }
}