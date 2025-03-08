using Application.Features.ProductReturns.Commands.Create;
using Application.Features.ProductReturns.Commands.Delete;
using Application.Features.ProductReturns.Commands.Update;
using Application.Features.ProductReturns.Queries.GetById;
using Application.Features.ProductReturns.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductReturnsController : BaseController
{
    //[HttpPost]
    //public async Task<ActionResult<CreatedProductReturnResponse>> Add([FromBody] CreateProductReturnCommand command)
    //{
    //    CreatedProductReturnResponse response = await Mediator.Send(command);

    //    return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    //}

    //[HttpPut]
    //public async Task<ActionResult<UpdatedProductReturnResponse>> Update([FromBody] UpdateProductReturnCommand command)
    //{
    //    UpdatedProductReturnResponse response = await Mediator.Send(command);

    //    return Ok(response);
    //}

    //[HttpDelete("{id}")]
    //public async Task<ActionResult<DeletedProductReturnResponse>> Delete([FromRoute] Guid id)
    //{
    //    DeleteProductReturnCommand command = new() { Id = id };

    //    DeletedProductReturnResponse response = await Mediator.Send(command);

    //    return Ok(response);
    //}

    //[HttpGet("{id}")]
    //public async Task<ActionResult<GetByIdProductReturnResponse>> GetById([FromRoute] Guid id)
    //{
    //    GetByIdProductReturnQuery query = new() { Id = id };

    //    GetByIdProductReturnResponse response = await Mediator.Send(query);

    //    return Ok(response);
    //}

    //[HttpGet]
    //public async Task<ActionResult<GetListResponse<GetListProductReturnListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    //{
    //    GetListProductReturnQuery query = new() { PageRequest = pageRequest };

    //    GetListResponse<GetListProductReturnListItemDto> response = await Mediator.Send(query);

    //    return Ok(response);
    //}
}