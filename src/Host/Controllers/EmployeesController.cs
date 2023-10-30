using Application.Common.Models;
using Application.Employees.Commands.ConvertEmployee;
using Application.Employees.Queries;
using Host.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[ApiController]
[EmployeeExceptionFilter]
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
    public async Task<IActionResult> Create(IFormFile? csvOrJsonFileContent, 
        [FromForm] string? csvOrJsonStringContent, 
        CancellationToken cancellationToken)
    {
        if (csvOrJsonFileContent is null && string.IsNullOrEmpty(csvOrJsonStringContent))
            return BadRequest();

        // NOTE: transaction 고민이 필요.
        if(csvOrJsonFileContent is not null)
            await this._mediator.Send(
                new ConvertFileEmployeeCommand(csvOrJsonFileContent), 
                cancellationToken
            );

        if (csvOrJsonStringContent is not null)
            await this._mediator.Send(
                new ConvertStringEmployeeCommand(csvOrJsonStringContent),
                cancellationToken
            );

        // TODO: value managed
        return Created(new Uri("/api/employee", UriKind.Relative), null);
    }

    [HttpGet("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeContactDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContactByName(string name, CancellationToken cancellationToken)
    {
        return Ok(await this._mediator.Send(new GetEmployeeContactQuery(name), cancellationToken));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<EmployeeDto>))]
    public async Task<IActionResult> GetPaginated([FromQuery] GetEmployeesWithPaginationQuery getEmployeesWithPaginationQuery, CancellationToken cancellationToken)
    {
        var employeeDtos = await this._mediator.Send(getEmployeesWithPaginationQuery, cancellationToken);

        return Ok(employeeDtos);
    }
}
