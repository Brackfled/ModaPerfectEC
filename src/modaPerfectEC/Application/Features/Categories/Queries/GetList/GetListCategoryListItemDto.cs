using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Categories.Queries.GetList;

public class GetListCategoryListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<SubCategory>? SubCategories { get; set; }
}