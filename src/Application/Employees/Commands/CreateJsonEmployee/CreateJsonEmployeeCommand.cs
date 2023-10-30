using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Application.Employees.Commands.CreateJsonEmployee;

public record CreateJsonEmployeeCommand : IRequest<int>
{
    public CreateJsonEmployeeCommand(string name, string email, string tel, string joined)
    {
        Name = name;
        Email = email;
        Tel = tel;
        Joined = joined;
    }

    [JsonPropertyName("name")]
    public string Name { get; init; }
    [JsonPropertyName("email")] 
    public string Email { get; init; }

    [JsonPropertyName("tel")]
    public string Tel { get; init; }
    [JsonPropertyName("joined")] 
    public string Joined { get; init; }
}

public class CreateJsonEmployeeCommandHandler : IRequestHandler<CreateJsonEmployeeCommand, int>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public CreateJsonEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this._employeeRepository = employeeRepository;
        this._mapper = mapper;
    }
    public async Task<int> Handle(CreateJsonEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee entity = _mapper.Map<Employee>(request);

        return await _employeeRepository.AddEmployeeAsync(entity, cancellationToken);
    }
}