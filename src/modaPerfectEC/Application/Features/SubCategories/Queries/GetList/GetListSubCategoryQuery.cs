using Application.Features.SubCategories.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.SubCategories.Constants.SubCategoriesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SubCategories.Queries.GetList;

public class GetListSubCategoryQuery : IRequest<GetListResponse<GetListSubCategoryListItemDto>>//, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    //public string[] Roles => [Admin, Read];

    public class GetListSubCategoryQueryHandler : IRequestHandler<GetListSubCategoryQuery, GetListResponse<GetListSubCategoryListItemDto>>
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMapper _mapper;

        public GetListSubCategoryQueryHandler(ISubCategoryRepository subCategoryRepository, IMapper mapper)
        {
            _subCategoryRepository = subCategoryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSubCategoryListItemDto>> Handle(GetListSubCategoryQuery request, CancellationToken cancellationToken)
        {
            IPaginate<SubCategory> subCategories = await _subCategoryRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSubCategoryListItemDto> response = _mapper.Map<GetListResponse<GetListSubCategoryListItemDto>>(subCategories);
            return response;
        }
    }
}