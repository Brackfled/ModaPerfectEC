using Application.Features.SubCategories.Constants;
using Application.Features.SubCategories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.SubCategories.Constants.SubCategoriesOperationClaims;

namespace Application.Features.SubCategories.Commands.Create;

public class CreateSubCategoryCommand : IRequest<CreatedSubCategoryResponse>, ISecuredRequest, ITransactionalRequest
{
    public required int CategoryId { get; set; }
    public required string Name { get; set; }

    public string[] Roles => [Admin, SubCategoriesOperationClaims.Create];

    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, CreatedSubCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly SubCategoryBusinessRules _subCategoryBusinessRules;

        public CreateSubCategoryCommandHandler(IMapper mapper, ISubCategoryRepository subCategoryRepository,
                                         SubCategoryBusinessRules subCategoryBusinessRules)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _subCategoryBusinessRules = subCategoryBusinessRules;
        }

        public async Task<CreatedSubCategoryResponse> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            await _subCategoryBusinessRules.SubCategoryShouldNotExists(request.Name);

            SubCategory subCategory = _mapper.Map<SubCategory>(request);

            await _subCategoryRepository.AddAsync(subCategory);

            CreatedSubCategoryResponse response = _mapper.Map<CreatedSubCategoryResponse>(subCategory);
            return response;
        }
    }
}