using Application.Features.SubCategories.Commands.Create;
using Application.Features.SubCategories.Commands.Delete;
using Application.Features.SubCategories.Commands.Update;
using Application.Features.SubCategories.Queries.GetById;
using Application.Features.SubCategories.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubCategoriesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedSubCategoryResponse>> Add([FromBody] CreateSubCategoryCommand command)
    {
        CreatedSubCategoryResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedSubCategoryResponse>> Update([FromBody] UpdateSubCategoryCommand command)
    {
        UpdatedSubCategoryResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedSubCategoryResponse>> Delete([FromRoute] int id)
    {
        DeleteSubCategoryCommand command = new() { Id = id };

        DeletedSubCategoryResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdSubCategoryResponse>> GetById([FromRoute] int id)
    {
        GetByIdSubCategoryQuery query = new() { Id = id };

        GetByIdSubCategoryResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListSubCategoryListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSubCategoryQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListSubCategoryListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}