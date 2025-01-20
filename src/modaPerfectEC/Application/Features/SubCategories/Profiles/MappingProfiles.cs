using Application.Features.SubCategories.Commands.Create;
using Application.Features.SubCategories.Commands.Delete;
using Application.Features.SubCategories.Commands.Update;
using Application.Features.SubCategories.Queries.GetById;
using Application.Features.SubCategories.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.SubCategories.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateSubCategoryCommand, SubCategory>();
        CreateMap<SubCategory, CreatedSubCategoryResponse>();

        CreateMap<UpdateSubCategoryCommand, SubCategory>();
        CreateMap<SubCategory, UpdatedSubCategoryResponse>();

        CreateMap<DeleteSubCategoryCommand, SubCategory>();
        CreateMap<SubCategory, DeletedSubCategoryResponse>();

        CreateMap<SubCategory, GetByIdSubCategoryResponse>();

        CreateMap<SubCategory, GetListSubCategoryListItemDto>();
        CreateMap<IPaginate<SubCategory>, GetListResponse<GetListSubCategoryListItemDto>>();
    }
}