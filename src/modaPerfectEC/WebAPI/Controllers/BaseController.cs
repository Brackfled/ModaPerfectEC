using MediatR;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Security.Extensions;

namespace WebAPI.Controllers;

public class BaseController : ControllerBase
{
    protected IMediator Mediator =>
        _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

    private IMediator? _mediator;

    protected string getIpAddress()
    {
        string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
                ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");
        return ipAddress;
    }

    protected Guid getUserIdFromRequest() //todo authentication behavior?
    {
        string? userIdString = HttpContext.User.GetIdClaim();

        if (userIdString is null)
            throw new AuthorizationException("You are not authenticated.");

        var userId = Guid.Parse(HttpContext.User.GetIdClaim()!);

        return userId;
    }
}
