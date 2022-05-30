using Lion.API.Controllers._Common;
using Lion.Core.Application.Sellers.Commands.Add;
using Lion.Core.Application.Sellers.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Lion.API.Controllers;

[Route("sellers")]
public class SellerController : ApiControllerBase
{
    [HttpGet]
    [Route("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json,
          AdditionalMediaTypeNames.Application.ProblemJson,
          Type = typeof(GetByIdResult))]
    [ProducesResponseType(typeof(GetByIdResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await Mediator.Send(new GetByIdQuery { SellerId = id });
        return Ok(result);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json,
              AdditionalMediaTypeNames.Application.ProblemJson,
              Type = typeof(AddResult))]
    [ProducesResponseType(typeof(AddResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] AddCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction("GetById", new { id = result.SellerId }, result);
    }
}
