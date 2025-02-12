using Application.Features.Categories.Constants;
using Application.Features.MPFile.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using MediaToolkit.Model;
using MediaToolkit;

namespace Application.Features.MPFile.Rules;
public class MPFileBusinessRules:BaseBusinessRules
{
    private readonly IProductImageRepository _productImageRepository;
    private readonly ICollectionVideoRepository _collectionVideoRepository;
    private readonly ILocalizationService _localizationService;

    public MPFileBusinessRules(IProductImageRepository productImageRepository, ICollectionVideoRepository collectionVideoRepository, ILocalizationService localizationService)
    {
        _productImageRepository = productImageRepository;
        _collectionVideoRepository = collectionVideoRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, MPFileBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task FileIsImageFiles(IList<IFormFile> formFiles)
    {
        string[] extensionList = { ".gif", ".png", ".jpg", ".jpeg" };

        foreach (IFormFile formFile in formFiles)
        {
            bool isSuccess = extensionList.Any(extensions => extensions.Contains(formFile.FileName.Substring(formFile.FileName.LastIndexOf("."))));
            if (!isSuccess)
                await throwBusinessException(MPFileBusinessMessages.FileIsNotImageFile);
        }
    }

    public async Task FileShouldBeMinAndMaxCount(IList<IFormFile> formFiles, int minCount,int maxCount)
    {
        if (formFiles.Count >= maxCount)
            await throwBusinessException(MPFileBusinessMessages.FilesCountMusBeBetweenTwoAndSix);
    }

    public async Task MPFileShouldExistsWhenSelected(ProductImage productImage)
    {
        if (productImage == null)
            await throwBusinessException(MPFileBusinessMessages.MPFileNotExists);
    }

    public async Task ProductHasAImageOverload(Product product, IList<IFormFile> formFiles, int maxCount)
    {
        if (product.ProductImages!.Count() + formFiles.Count() > maxCount)
            await throwBusinessException(MPFileBusinessMessages.ImageOverload);
    }

    public async Task FileSizeIsCorrect(IFormFile formFile, int maxWidth, int maxHeight)
    {

        using (var stream = formFile.OpenReadStream())
        {
            using (var image = System.Drawing.Image.FromStream(stream))
            {
                if (image.Width > maxWidth || image.Height > maxHeight)
                    await throwBusinessException(MPFileBusinessMessages.FileIsIncorrect);
            }
        }
    }

    public async Task CollectionVideoShouldExistsWhenSelected(CollectionVideo collectionVideo)
    {
        bool doesExists = await _collectionVideoRepository.AnyAsync(cv => cv.Id == collectionVideo.Id);
        if (!doesExists)
            await throwBusinessException(MPFileBusinessMessages.MPFileNotExists);
    }

    public async Task CollectionVideoRequestCountMustBeCorrectCount(IList<IFormFile> formFiles, int count)
    {
        if (formFiles.Count != count)
            await throwBusinessException(MPFileBusinessMessages.FileIsIncorrect);
    }

    public async Task FileIsVideoFile(IFormFile formFile)
    {
        string[] extensionList = { ".mp4" , ".mov", ".avi"};
        bool isSuccess = extensionList.Any(extensions => extensions.Contains(formFile.FileName.Substring(formFile.FileName.LastIndexOf("."))));
        if (!isSuccess)
            await throwBusinessException(MPFileBusinessMessages.FileIsNotVideoFile);
    }

    public async Task VideoFileIsCorrect(IFormFile formFile, int maxWidth, int maxHeight, int maxSize)
    {
        FileInfo fileInfo = new FileInfo(formFile.FileName);
        long fileSizeMb = fileInfo.Length / (1024 * 1024);

        if (fileSizeMb > maxSize)
            await throwBusinessException(MPFileBusinessMessages.CollectionVideoFileSizeIsOver);

        
    }
}
