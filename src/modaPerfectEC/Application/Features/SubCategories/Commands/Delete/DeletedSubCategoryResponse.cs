using NArchitecture.Core.Application.Responses;

namespace Application.Features.SubCategories.Commands.Delete;

public class DeletedSubCategoryResponse : IResponse
{
    public int Id { get; set; }
}