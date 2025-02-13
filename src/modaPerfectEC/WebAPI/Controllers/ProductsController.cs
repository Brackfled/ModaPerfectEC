using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Services.ProductImages;
using Domain.Entities;
using Application.Services.Stroage;
using Domain.Dtos;
using Application.Features.Products.Queries.GetAllByFiltered;
using NArchitecture.Core.Persistence.Dynamic;
using Application.Features.Products.Queries.GetListByDynamic;
using Application.Features.Products.Queries.GetListByShowCase;
using Domain.Enums;
using Application.Services.ExchangeService;
using Application.Features.Products.Queries.GetListByCategoryId;
using Application.Features.Products.Queries.GetListBySubCategoryId;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{
    private readonly ExchangeServiceBase _exchangeService;

    public ProductsController(ExchangeServiceBase exchangeService)
    {
        _exchangeService = exchangeService;
    }

    [HttpPost]
    public async Task<ActionResult<CreatedProductResponse>> Add([FromBody] CreateProductRequest createProductRequest)
    {
        CreateProductCommand command = new() { CreateProductRequest = createProductRequest };
        CreatedProductResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedProductResponse>> Update([FromBody] UpdateProductCommand command)
    {
        UpdatedProductResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedProductResponse>> Delete([FromRoute] Guid id)
    {
        DeleteProductCommand command = new() { Id = id };

        DeletedProductResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdProductResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdProductQuery query = new() { Id = id };

        GetByIdProductResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListProductListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProductQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListProductListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductState? productState)
    {
        GetAllByFilteredProductQuery query = new() { ProductState = productState };
        ICollection<GetAllByFilteredProductListItemDto> result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("GetListByDynamic")]
    public async Task<IActionResult> GetListByDynamicProducts( GetListByDynamicProductRequest getListByDynamicProductRequest)
    {
        GetListByDynamicProductQuery query = new() { GetListByDynamicProductRequest = getListByDynamicProductRequest};
        ICollection<GetListByDynamicProductListItemDto> result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetAllByShowCase")]
    public async Task<IActionResult> GetAllByShowCaseProducts()
    {
        GetListByShowCaseProductQuery query = new();
        ICollection<GetListByShowCaseProductListItemDto> result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetListByCategoryName")]
    public async Task<IActionResult> GetListByCategoryIdProduts([FromQuery] string categoryName)
    {
        GetListByCategoryIdProductQuery  query = new() { Name= categoryName };

        ICollection<GetListByCategoryIdProductListItemDto> result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetListBySubCategoryName")]
    public async Task<IActionResult> GetListBySubCategoryProducts([FromQuery] string subCategoryName)
    {
        GetListBySubCategoryIdProductQuery query = new() { Name = subCategoryName};
        ICollection<GetListBySubCategoryIdProductListItemDto> result = await Mediator.Send(query);
        return Ok(result);
    }
}