using Application.Employees.Commands.ConvertEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[ApiController]
[Route("api/employee")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create(IFormFile? csvOrJsonFileContent, 
        [FromForm] string? csvOrJsonStringContent, 
        CancellationToken cancellationToken)
    {
        if (csvOrJsonFileContent is null && string.IsNullOrEmpty(csvOrJsonStringContent))
            return BadRequest();

        // NOTE: transaction 고민이 필요.
        await this._mediator.Send(
            new ConvertFileEmployeeCommand(csvOrJsonFileContent), 
            cancellationToken
        );

        await this._mediator.Send(
            new ConvertStringEmployeeCommand(csvOrJsonStringContent),
            cancellationToken
        );

        // TODO: value managed
        return Created(new Uri("/api/employee", UriKind.Relative), null);
    }
}
