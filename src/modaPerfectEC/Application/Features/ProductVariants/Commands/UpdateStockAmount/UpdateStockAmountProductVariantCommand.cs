using Amazon.Runtime.Internal;
using Application.Features.Products.Constants;
using Application.Features.ProductVariants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Products.Constants.ProductsOperationClaims;


namespace Application.Features.ProductVariants.Commands.UpdateStockAmount;
public class UpdateStockAmountProductVariantCommand: IRequest<UpdatedStockAmountProductVariantResponse>, ITransactionalRequest, ISecuredRequest
{
    public UpdateStockAmountProductVariantRequest UpdateStockAmountProductVariantRequest { get; set; }

    public string[] Roles => [Admin, ProductsOperationClaims.Update];

    public class UpdateStockAmountProductVariantCommandHandler: IRequestHandler<UpdateStockAmountProductVariantCommand, UpdatedStockAmountProductVariantResponse>
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;
        private IMapper _mapper;

        public UpdateStockAmountProductVariantCommandHandler(IProductVariantRepository productVariantRepository, ProductVariantBusinessRules productVariantBusinessRules, IMapper mapper)
        {
            _productVariantRepository = productVariantRepository;
            _productVariantBusinessRules = productVariantBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdatedStockAmountProductVariantResponse> Handle(UpdateStockAmountProductVariantCommand request, CancellationToken cancellationToken)
        {
            ProductVariant? productVariant = await _productVariantRepository.GetAsync(p => p.Id == request.UpdateStockAmountProductVariantRequest.Id);

            await _productVariantBusinessRules.ProductVariantShouldExistWhenSelected(productVariant);

            ProductVariant updatedProductVariant = await _productVariantRepository.UpdateStockAmount(productVariant, request.UpdateStockAmountProductVariantRequest.Increase, request.UpdateStockAmountProductVariantRequest.ProcessAmount);

            UpdatedStockAmountProductVariantResponse response = _mapper.Map<UpdatedStockAmountProductVariantResponse>(updatedProductVariant);
            return response;
        }
    }
}
