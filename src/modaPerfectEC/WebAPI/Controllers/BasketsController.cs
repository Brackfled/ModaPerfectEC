using Application.Features.Baskets.Commands.Create;
using Application.Features.Baskets.Commands.Delete;
using Application.Features.Baskets.Commands.Update;
using Application.Features.Baskets.Queries.GetById;
using Application.Features.Baskets.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Baskets.Queries.GetFromAuth;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketsController : BaseController
{
    //[HttpPost]
    //public async Task<ActionResult<CreatedBasketResponse>> Add([FromBody] CreateBasketCommand command)
    //{
    //    CreatedBasketResponse response = await Mediator.Send(command);

    //    return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    //}

    //[HttpPut]
    //public async Task<ActionResult<UpdatedBasketResponse>> Update([FromBody] UpdateBasketCommand command)
    //{
    //    UpdatedBasketResponse response = await Mediator.Send(command);

    //    return Ok(response);
    //}

    //[HttpDelete("{id}")]
    //public async Task<ActionResult<DeletedBasketResponse>> Delete([FromRoute] Guid id)
    //{
    //    DeleteBasketCommand command = new() { Id = id };

    //    DeletedBasketResponse response = await Mediator.Send(command);

    //    return Ok(response);
    //}

    //[HttpGet("{id}")]
    //public async Task<ActionResult<GetByIdBasketResponse>> GetById([FromRoute] Guid id)
    //{
    //    GetByIdBasketQuery query = new() { Id = id };

    //    GetByIdBasketResponse response = await Mediator.Send(query);

    //    return Ok(response);
    //}

    //[HttpGet]
    //public async Task<ActionResult<GetListResponse<GetListBasketListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    //{
    //    GetListBasketQuery query = new() { PageRequest = pageRequest };

    //    GetListResponse<GetListBasketListItemDto> response = await Mediator.Send(query);

    //    return Ok(response);
    //}

    [HttpGet("GetFromAuth")]
    public async Task<IActionResult> GetFromAuth()
    {
        GetFromAuthBasketQuery query = new() { UserId = getUserIdFromRequest() };
        GetFromAuthBasketResponse result = await Mediator.Send(query);
        return Ok(result);
    }
}