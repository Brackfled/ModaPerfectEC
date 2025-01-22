using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Categories.Queries.GetById;

public class GetByIdCategoryResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<SubCategory>? SubCategories { get; set; }
}