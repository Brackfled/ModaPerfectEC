using Application.Features.Categories.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Categories.Constants.CategoriesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Queries.GetList;

public class GetListCategoryQuery : IRequest<GetListResponse<GetListCategoryListItemDto>>//, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    //public string[] Roles => [Admin, Read];

    public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, GetListResponse<GetListCategoryListItemDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCategoryListItemDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Category> categories = await _categoryRepository.GetListAsync(
                include:c => c.Include(c => c.SubCategories)!,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCategoryListItemDto> response = _mapper.Map<GetListResponse<GetListCategoryListItemDto>>(categories);
            return response;
        }
    }
}