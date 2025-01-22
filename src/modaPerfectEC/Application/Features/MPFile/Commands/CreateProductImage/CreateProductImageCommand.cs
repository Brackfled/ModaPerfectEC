using Amazon.Runtime.Internal;
using Application.Features.MPFile.Constants;
using Application.Features.MPFile.Rules;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using Application.Services.Stroage;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    public Guid ProductId { get; set; }
    public IList<IFormFile> FormFiles { get; set; }

    public string[] Roles => [MPFilesOperationClaims.Create];

    public class CreateProductImageCommandHandler: IRequestHandler<CreateProductImageCommand, CreatedProductImageResponse>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IStroageService _stroageService;
        private readonly ProductBusinessRules _productBusinessRules;
        private readonly MPFileBusinessRules _mpFileBusinessRules;

        public CreateProductImageCommandHandler(IProductImageRepository productImageRepository, IStroageService stroageService, ProductBusinessRules productBusinessRules, MPFileBusinessRules mpFileBusinessRules)
        {
            _productImageRepository = productImageRepository;
            _stroageService = stroageService;
            _productBusinessRules = productBusinessRules;
            _mpFileBusinessRules = mpFileBusinessRules;
        }

        public async Task<CreatedProductImageResponse> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            await _mpFileBusinessRules.FileShouldBeMinAndMaxCount(request.FormFiles,2,6);
            await _mpFileBusinessRules.FileIsImageFiles(request.FormFiles);
            await _productBusinessRules.ProductIdShouldExistWhenSelected(request.ProductId, cancellationToken);

            IList<ProductImage> productImages = new List<ProductImage>();

            foreach (IFormFile formFile in request.FormFiles)
            {
                Domain.Dtos.FileOptions fileOptions = await _stroageService.UploadFileAsync(formFile, "hk-image");

                ProductImage productImage = new()
                {
                    Id = Guid.NewGuid(),
                    ProductId = request.ProductId,
                    FileName = fileOptions.FileName,
                    FilePath = fileOptions.BucketName,
                    FileUrl = fileOptions.FileUrl,
                };

                ProductImage addedProductImage = await _productImageRepository.AddAsync(productImage);
                productImages.Add(addedProductImage);
            }

            CreatedProductImageResponse response = new() { ProductId =  request.ProductId , ProductImages = productImages};
            return response;
        }
    }
}
