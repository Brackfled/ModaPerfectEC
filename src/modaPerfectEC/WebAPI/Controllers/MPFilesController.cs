using Application.Features.MPFile.Commands.CreateProductImage;
using Application.Features.MPFile.Commands.DeleteProductImage;
using Application.Features.Products.Commands.Delete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MPFilesController : BaseController
{
    [HttpPost("CreateProductImage")]
    public async Task<IActionResult> CreateProductImage([FromForm] CreateProductImageCommand command)
    {
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
}
