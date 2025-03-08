using NArchitecture.Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.ProductReturns.Queries.GetList;

public class GetListProductReturnListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BasketItemId { get; set; }
    public Guid OrderId { get; set; }
    public string Description { get; set; }
    public ReturnState ReturnState { get; set; }
}