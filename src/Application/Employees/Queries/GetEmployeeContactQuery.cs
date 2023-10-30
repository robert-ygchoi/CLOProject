using Application.Common.Exceptions;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Employees.Queries;

public record GetEmployeeContactQuery:  IRequest<EmployeeContactDto>
{
    public GetEmployeeContactQuery(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
}

public class GetEmployeeContactQueryHandler : IRequestHandler<GetEmployeeContactQuery, EmployeeContactDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeContactQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this._employeeRepository = employeeRepository;
        this._mapper = mapper;
    }
    public async Task<EmployeeContactDto> Handle(GetEmployeeContactQuery request, CancellationToken cancellationToken)
    {
        var entity = await _employeeRepository.GetEmployeeByNameAsync(request.Name, cancellationToken);

        if (entity is null)
            throw new EntityNotFoundException(request.Name);

        return _mapper.Map<EmployeeContactDto>(entity);
    }
}


