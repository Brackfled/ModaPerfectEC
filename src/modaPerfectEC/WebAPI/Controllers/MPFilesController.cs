using Application.Features.MPFile.Commands.CreateProductImage;
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
}
