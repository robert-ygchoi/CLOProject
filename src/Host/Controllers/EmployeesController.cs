using Application.Employees.Commands.CreateEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[ApiController]
[Route("api/employee")]
public class EmployeesController
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
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
    {
        await this._mediator.Send(command);
        return new CreatedResult(new Uri("/api/employee", UriKind.Relative), new { name = command.Name });
    }
}
