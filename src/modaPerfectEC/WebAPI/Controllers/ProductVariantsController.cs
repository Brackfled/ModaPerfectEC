using Application.Features.ProductVariants.Commands.Create;
using Application.Features.ProductVariants.Commands.Delete;
using Application.Features.ProductVariants.Commands.Update;
using Application.Features.ProductVariants.Queries.GetById;
using Application.Features.ProductVariants.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductVariantsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedProductVariantResponse>> Add([FromBody] CreateProductVariantCommand command)
    {
        CreatedProductVariantResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedProductVariantResponse>> Update([FromBody] UpdateProductVariantRequest updateProductVariantRequest)
    {
        UpdateProductVariantCommand command = new() { UpdateProductVariantRequest = updateProductVariantRequest};

        UpdatedProductVariantResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedProductVariantResponse>> Delete([FromRoute] Guid id)
    {
        DeleteProductVariantCommand command = new() { Id = id };

        DeletedProductVariantResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdProductVariantResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdProductVariantQuery query = new() { Id = id };

        GetByIdProductVariantResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListProductVariantListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProductVariantQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListProductVariantListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}