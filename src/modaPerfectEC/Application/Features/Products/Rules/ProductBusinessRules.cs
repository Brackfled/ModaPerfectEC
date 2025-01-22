using Application.Features.Products.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;

namespace Application.Features.Products.Rules;

public class ProductBusinessRules : BaseBusinessRules
{
    private readonly IProductRepository _productRepository;
    private readonly ILocalizationService _localizationService;

    public ProductBusinessRules(IProductRepository productRepository, ILocalizationService localizationService)
    {
        _productRepository = productRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ProductsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ProductShouldExistWhenSelected(Product? product)
    {
        if (product == null)
            await throwBusinessException(ProductsBusinessMessages.ProductNotExists);
    }

    public async Task ProductIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ProductShouldExistWhenSelected(product);
    }

    public async Task ProductNameShouldNotExistsWhenSelected(string name)
    {
        bool doesExists = await _productRepository.AnyAsync(p => p.Name == name);

        if (doesExists)
            await throwBusinessException(ProductsBusinessMessages.ProductExists);
    }

    public async Task IsTheImageTheFinalImageOfTheProduct(Guid productId)
    {
        Product? product = await _productRepository.GetAsync(p => p.Id == productId, include:opt => opt.Include(p => p.ProductImages!));

        if (product!.ProductImages!.Count() <= 1)
            await throwBusinessException(ProductsBusinessMessages.TheLastImageCannotBeDeleted);
    }
}