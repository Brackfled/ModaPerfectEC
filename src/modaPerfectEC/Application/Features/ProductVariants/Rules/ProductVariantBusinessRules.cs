using Application.Features.ProductVariants.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Amazon.S3.Model;
using Application.Features.Products.Constants;

namespace Application.Features.ProductVariants.Rules;

public class ProductVariantBusinessRules : BaseBusinessRules
{
    private readonly IProductVariantRepository _productVariantRepository;
    private readonly ILocalizationService _localizationService;

    public ProductVariantBusinessRules(IProductVariantRepository productVariantRepository, ILocalizationService localizationService)
    {
        _productVariantRepository = productVariantRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ProductVariantsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ProductVariantShouldExistWhenSelected(ProductVariant? productVariant)
    {
        if (productVariant == null)
            await throwBusinessException(ProductVariantsBusinessMessages.ProductVariantNotExists);
    }

    public async Task ProductVariantIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ProductVariant? productVariant = await _productVariantRepository.GetAsync(
            predicate: pv => pv.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ProductVariantShouldExistWhenSelected(productVariant);
    }

    public async Task ProductHasAAssociatedProductVariant(Guid productId, string color)
    {
        bool doesExists = await _productVariantRepository.AnyAsync(pv => pv.ProductId == productId && pv.Color == color);

        if (doesExists)
            await throwBusinessException(ProductVariantsBusinessMessages.ThisProductHasAThisVariant);
    }

    public async Task SizesShouldBeTheRight(int[] sizes)
    {
        if (sizes.Length > 9)
            await throwBusinessException(ProductVariantsBusinessMessages.SizesOverLenght);

        bool hasDuplicated = (sizes.Count() != sizes.Distinct().Count());

        if (hasDuplicated)
            await throwBusinessException(ProductVariantsBusinessMessages.SizesHasDuplicatedElement);

        foreach (int size in sizes)
        {
            if (size < 38 && size > 54)
                await throwBusinessException(ProductVariantsBusinessMessages.SizesListHasAIllegalSize);

            if(size % 2 != 0)
                await throwBusinessException(ProductVariantsBusinessMessages.SizesListHasAIllegalSize);

        }

       
    }

    public async Task DecreaseIsAvailable(ProductVariant productVariant, int processAmount)
    {
        if (productVariant.StockAmount - processAmount < 0)
            await throwBusinessException(ProductsBusinessMessages.DecreaseIsNotPossible);
    }
}