namespace Application.Features.Orders.Constants;

public static class OrdersOperationClaims
{
    private const string _section = "Orders";

    public const string Admin = $"{_section}.Admin";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
    public const string GetById = $"{_section}.GetById";
    public const string GetListFromAuth = $"{_section}.GetListFromAuth";
}