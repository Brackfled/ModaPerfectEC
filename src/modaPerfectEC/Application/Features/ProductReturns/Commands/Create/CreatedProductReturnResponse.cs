using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.ProductReturns.Commands.Create;

public class CreatedProductReturnResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BasketItemId { get; set; }
    public Guid OrderId { get; set; }
    public string Description { get; set; }
    public ReturnState ReturnState { get; set; }
}