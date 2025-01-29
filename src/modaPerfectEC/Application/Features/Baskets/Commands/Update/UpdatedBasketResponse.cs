using NArchitecture.Core.Application.Responses;

namespace Application.Features.Baskets.Commands.Update;

public class UpdatedBasketResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double TotalPrice { get; set; }
    public bool IsOrderBasket { get; set; }
}