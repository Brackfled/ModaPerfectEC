using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductReturns.Commands.Delete;

public class DeletedProductReturnResponse : IResponse
{
    public Guid Id { get; set; }
}