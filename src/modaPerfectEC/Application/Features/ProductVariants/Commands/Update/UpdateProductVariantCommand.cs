using Application.Features.ProductVariants.Constants;
using Application.Features.ProductVariants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ProductVariants.Constants.ProductVariantsOperationClaims;
using Application.Services.Products;

namespace Application.Features.ProductVariants.Commands.Update;

public class UpdateProductVariantCommand : IRequest<UpdatedProductVariantResponse>, ITransactionalRequest //, ISecuredRequest
{
    public UpdateProductVariantRequest UpdateProductVariantRequest { get; set; }

//    public string[] Roles => [Admin, Write, ProductVariantsOperationClaims.Update];

    public class UpdateProductVariantCommandHandler : IRequestHandler<UpdateProductVariantCommand, UpdatedProductVariantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;
        private readonly IProductService _productService;

        public UpdateProductVariantCommandHandler(IMapper mapper, IProductVariantRepository productVariantRepository, ProductVariantBusinessRules productVariantBusinessRules, IProductService productService)
        {
            _mapper = mapper;
            _productVariantRepository = productVariantRepository;
            _productVariantBusinessRules = productVariantBusinessRules;
            _productService = productService;
        }

        public async Task<UpdatedProductVariantResponse> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
        {
            if(request.UpdateProductVariantRequest.Sizes != null)
                await _productVariantBusinessRules.SizesShouldBeTheRight(request.UpdateProductVariantRequest.Sizes!);

            ProductVariant? productVariant = await _productVariantRepository.GetAsync(predicate: pv => pv.Id == request.UpdateProductVariantRequest.Id, cancellationToken: cancellationToken);
            await _productVariantBusinessRules.ProductVariantShouldExistWhenSelected(productVariant);
            productVariant = _mapper.Map(request.UpdateProductVariantRequest, productVariant);

            await _productVariantRepository.UpdateAsync(productVariant!);

            Product? product = await _productService.GetAsync(p => p.Id == productVariant!.ProductId);

            UpdatedProductVariantResponse response = _mapper.Map<UpdatedProductVariantResponse>(productVariant);
            response.Product = product;
            return response;
        }
    }
}