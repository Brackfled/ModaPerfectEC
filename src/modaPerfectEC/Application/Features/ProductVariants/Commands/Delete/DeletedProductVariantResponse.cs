using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductVariants.Commands.Delete;

public class DeletedProductVariantResponse : IResponse
{
    public Guid Id { get; set; }
}