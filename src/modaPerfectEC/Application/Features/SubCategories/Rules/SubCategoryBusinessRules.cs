using Application.Features.SubCategories.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.SubCategories.Rules;

public class SubCategoryBusinessRules : BaseBusinessRules
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ILocalizationService _localizationService;

    public SubCategoryBusinessRules(ISubCategoryRepository subCategoryRepository, ILocalizationService localizationService)
    {
        _subCategoryRepository = subCategoryRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, SubCategoriesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task SubCategoryShouldExistWhenSelected(SubCategory? subCategory)
    {
        if (subCategory == null)
            await throwBusinessException(SubCategoriesBusinessMessages.SubCategoryNotExists);
    }

    public async Task SubCategoryIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        SubCategory? subCategory = await _subCategoryRepository.GetAsync(
            predicate: sc => sc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SubCategoryShouldExistWhenSelected(subCategory);
    }

    public async Task SubCategoryShouldNotExists(string name)
    {
        bool doesExists = await _subCategoryRepository.AnyAsync(sc => sc.Name == name);
        if (doesExists)
            await throwBusinessException(SubCategoriesBusinessMessages.SubCategoryNotExists);
    }
}