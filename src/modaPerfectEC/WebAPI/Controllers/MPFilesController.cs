using Application.Features.MPFile.Commands.CreateProductImage;
using Application.Features.MPFile.Commands.DeleteProductImage;
using Application.Features.MPFile.Queries.GetById;
using Application.Features.MPFile.Queries.GetListByProductId;
using Application.Features.Products.Commands.Delete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MPFilesController : BaseController
{
    [HttpPost("CreateProductImage")]
    public async Task<IActionResult> CreateProductImage([FromForm] CreateProductImageRequest createProductImageRequest)
    {
        CreateProductImageCommand command = new() { CreateProductImageRequest = createProductImageRequest };
        CreatedProductImageResponse result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("DeleteProductImage/{id}")]
    public async Task<IActionResult> DeleteProductImage([FromRoute] Guid id)
    {
        DeleteProductImageCommand command = new() { Id = id};
        DeletedProductImageResponse result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetByIdProduct([FromRoute] Guid id)
    {
        GetByIdProductImageQuery query = new() { Id = id };
        GetByIdProductImageResponse result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetListByProductId/{productId}")]
    public async Task<IActionResult> GetListByProductIdProductImage([FromRoute] Guid productId)
    {
        GetListByProductIdProductImageQuery query = new() { ProductId = productId };
        ICollection<GetListByProductIdProductImageListItemDto> result = await Mediator.Send(query);
        return Ok(result);
    }
}
