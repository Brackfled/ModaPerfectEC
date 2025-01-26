using Amazon.Runtime.Internal;
using Application.Features.MPFile.Constants;
using Application.Features.MPFile.Rules;
using Application.Features.Products.Rules;
using Application.Services.Products;
using Application.Services.Repositories;
using Application.Services.Stroage;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.CreateProductImage;
public class CreateProductImageCommand: IRequest<CreatedProductImageResponse>, ITransactionalRequest, ISecuredRequest
{
    public CreateProductImageRequest CreateProductImageRequest { get; set; }

    public string[] Roles => [MPFilesOperationClaims.Create];

    public class CreateProductImageCommandHandler: IRequestHandler<CreateProductImageCommand, CreatedProductImageResponse>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IStroageService _stroageService;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly MPFileBusinessRules _mpFileBusinessRules;
        private readonly IProductService _productService;

        public CreateProductImageCommandHandler(IProductImageRepository productImageRepository, IStroageService stroageService, ProductBusinessRules productBusinessRules, MPFileBusinessRules mpFileBusinessRules, IProductService productService)
        {
            _productImageRepository = productImageRepository;
            _stroageService = stroageService;
            _productBusinessRules = productBusinessRules;
            _mpFileBusinessRules = mpFileBusinessRules;
            _productService = productService;
        }

        public async Task<CreatedProductImageResponse> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            await _mpFileBusinessRules.FileShouldBeMinAndMaxCount(request.CreateProductImageRequest.FormFiles,2,6);
            await _mpFileBusinessRules.FileIsImageFiles(request.CreateProductImageRequest.FormFiles);
            await _productBusinessRules.ProductIdShouldExistWhenSelected(request.CreateProductImageRequest.ProductId, cancellationToken);

            Product? product = await _productService.GetAsync(p => p.Id == request.CreateProductImageRequest.ProductId, include: opt => opt.Include(p => p.ProductImages)!);

            await _mpFileBusinessRules.ProductHasAImageOverload(product!, request.CreateProductImageRequest.FormFiles, 6);

            IList<ProductImage> productImages = new List<ProductImage>();

            foreach (IFormFile formFile in request.CreateProductImageRequest.FormFiles)
            {
                Domain.Dtos.FileOptions fileOptions = await _stroageService.UploadFileAsync(formFile, "hk-image");

                ProductImage productImage = new()
                {
                    Id = Guid.NewGuid(),
                    ProductId = request.CreateProductImageRequest.ProductId,
                    FileName = fileOptions.FileName,
                    FilePath = fileOptions.BucketName,
                    FileUrl = fileOptions.FileUrl,
                };

                ProductImage addedProductImage = await _productImageRepository.AddAsync(productImage);
                productImages.Add(addedProductImage);
            }

            CreatedProductImageResponse response = new() { ProductId =  request.CreateProductImageRequest.ProductId , ProductImages = productImages};
            return response;
        }
    }
}
