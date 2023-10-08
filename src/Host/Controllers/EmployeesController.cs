using Application.Employees.Commands.CreateEmployee;
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
        if (csvOrJsonFileContent is null || string.IsNullOrEmpty(csvOrJsonStringContent))
            return BadRequest();

        // TODO: csvOrJsonStringContent 처리
        var employees = await this._mediator.Send(
            new CreateEmployeeFileCommand() { JsonOrCsvFileContent = csvOrJsonFileContent }, 
            cancellationToken
        );

        foreach (var employee in employees)
        {
            await this._mediator.Send(employee, cancellationToken);
        }

        return Created(new Uri("/api/employee", UriKind.Relative), new { employees.Count });
    }
}
