using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lion.API.Controllers._Common;

public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
