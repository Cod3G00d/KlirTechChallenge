﻿using KlirTechChallenge.Application.Products;
using KlirTechChallenge.WebApi.Controllers.Base;
using KlirTechChallenge.Application.Products.ListProducts;

namespace KlirTechChallenge.WebApi.Controllers;

[Authorize]
[Route("api/products")]
[ApiController]
public class ProductsController : BaseController
{
    public ProductsController(
        IMediator mediator)
        : base(mediator)
    {
    }

    [HttpGet, Route("{currency}")]
    [Authorize(Policy = "CanRead")]
    [ProducesResponseType(typeof(IList<ProductViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProducts([FromRoute]string currency)
    {
        var query = new ListProductsQuery(currency);
        return await Response(query);
    }
}