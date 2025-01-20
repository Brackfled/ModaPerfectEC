using NArchitecture.Core.Application.Responses;

namespace Application.Features.SubCategories.Commands.Create;

public class CreatedSubCategoryResponse : IResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
}