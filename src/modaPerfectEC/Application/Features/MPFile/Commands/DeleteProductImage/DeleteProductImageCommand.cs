using Amazon.Runtime.Internal;
using Application.Features.MPFile.Constants;
using Application.Features.MPFile.Rules;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using Application.Services.Stroage;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.DeleteProductImage;
public class DeleteProductImageCommand: IRequest<DeletedProductImageResponse>, ITransactionalRequest, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [MPFilesOperationClaims.Deleted];

    public class DeleteProductImageCommandHandler: IRequestHandler<DeleteProductImageCommand, DeletedProductImageResponse>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IStroageService _stroageService;
        private IMapper _mapper;
        private readonly MPFileBusinessRules _mPFileBusinessRules;
        private readonly ProductBusinessRules _productBusinessRules;

        public DeleteProductImageCommandHandler(IProductImageRepository productImageRepository, IStroageService stroageService, IMapper mapper, MPFileBusinessRules mPFileBusinessRules, ProductBusinessRules productBusinessRules)
        {
            _productImageRepository = productImageRepository;
            _stroageService = stroageService;
            _mapper = mapper;
            _mPFileBusinessRules = mPFileBusinessRules;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<DeletedProductImageResponse> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {

            ProductImage? productImage = await _productImageRepository.GetAsync(
                    predicate: pi => pi.Id == request.Id,
                    cancellationToken:cancellationToken
                );

            await _mPFileBusinessRules.MPFileShouldExistsWhenSelected(productImage!);
            await _productBusinessRules.IsTheImageTheFinalImageOfTheProduct(productImage!.ProductId);

            await _stroageService.DeleteFileAsync(productImage!.FilePath, productImage.FileName);

            ProductImage deletedProductImage = await _productImageRepository.DeleteAsync(productImage,true);

            DeletedProductImageResponse response = _mapper.Map<DeletedProductImageResponse>(deletedProductImage);
            return response;

        }
    }
}
