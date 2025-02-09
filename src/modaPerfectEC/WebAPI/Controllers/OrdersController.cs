using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Queries.GetById;
using Application.Features.Orders.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Features.Orders.Queries.GetListAll;
using Application.Features.Orders.Queries.GetListFromAuth;
using NArchitecture.Core.Mailing;
using MimeKit;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : BaseController
{
    private readonly IMailService _mailService;

    public OrdersController(IMailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost]
    public async Task<ActionResult<CreatedOrderResponse>> Add([FromBody] CreateOrderRequest createOrderRequest)
    {
        CreateOrderCommand command = new() { CreateOrderRequest = createOrderRequest, UserId = getUserIdFromRequest() };
        CreatedOrderResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedOrderResponse>> Update([FromBody] UpdateOrderCommand command)
    {
        UpdatedOrderResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedOrderResponse>> Delete([FromRoute] Guid id)
    {
        DeleteOrderCommand command = new() { Id = id };

        DeletedOrderResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdOrderResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdOrderQuery query = new() { Id = id };

        GetByIdOrderResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    //[HttpGet]
    //public async Task<ActionResult<GetListResponse<GetListOrderListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    //{
    //    GetListOrderQuery query = new() { PageRequest = pageRequest };

    //    GetListResponse<GetListOrderListItemDto> response = await Mediator.Send(query);

    //    return Ok(response);
    //}

    [HttpGet("GetListAll")]
    public async Task<IActionResult> GetListAllOrders()
    {
        ICollection<GetListAllOrderListItemDto> result = await Mediator.Send(new GetListAllOrderQuery());
        return Ok(result);
    }

    [HttpGet("GetListFromAuth")]
    public async Task<IActionResult> GetListFromAuth()
    {
        GetListFromAuthOrderQuery query = new() { UserId = getUserIdFromRequest() };
        ICollection<GetListFromAuthOrderListItemDto> result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("MailDeneme")]
    public async Task<ActionResult<bool>> MailDeneme()
    {
        List<MailboxAddress> mails = new List<MailboxAddress> { new MailboxAddress(name:"oncellhsyn@outlook.com", "oncellhsyn@outlook.com") };

       await _mailService.SendEmailAsync( new Mail
       {
           ToList = mails,
           Subject = "Deneme",
           HtmlBody="<b>Merhabalar</b> Efenim"
       });

        return Ok(true);
    }
}