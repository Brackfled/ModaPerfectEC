using Application.Features.ProductReturns.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.ProductReturns.Rules;

public class ProductReturnBusinessRules : BaseBusinessRules
{
    private readonly IProductReturnRepository _productReturnRepository;
    private readonly ILocalizationService _localizationService;

    public ProductReturnBusinessRules(IProductReturnRepository productReturnRepository, ILocalizationService localizationService)
    {
        _productReturnRepository = productReturnRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ProductReturnsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ProductReturnShouldExistWhenSelected(ProductReturn? productReturn)
    {
        if (productReturn == null)
            await throwBusinessException(ProductReturnsBusinessMessages.ProductReturnNotExists);
    }

    public async Task ProductReturnIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ProductReturn? productReturn = await _productReturnRepository.GetAsync(
            predicate: pr => pr.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ProductReturnShouldExistWhenSelected(productReturn);
    }
}