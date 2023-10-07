using Application.Employees.Commands.CreateEmployee;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

public class EmployeesController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create(CreateEmployeeCommand command)
    {
        throw new NotImplementedException();
    }
}
