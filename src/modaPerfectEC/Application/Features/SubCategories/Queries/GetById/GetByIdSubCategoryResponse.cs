using NArchitecture.Core.Application.Responses;

namespace Application.Features.SubCategories.Queries.GetById;

public class GetByIdSubCategoryResponse : IResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
}