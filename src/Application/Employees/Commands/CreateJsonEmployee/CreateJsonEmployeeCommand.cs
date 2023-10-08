using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.Employees.Commands.CreateJsonEmployee;

public record CreateJsonEmployeeCommand(
    [property: JsonPropertyName("name")] string Name, 
    [property: JsonPropertyName("email")] string Email, 
    [property: JsonPropertyName("tel")] string Tel,
    [property: JsonPropertyName("joined")] string Joined) : IRequest<int>;

public class CreateJsonEmployeeCommandHandler : IRequestHandler<CreateJsonEmployeeCommand, int>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public CreateJsonEmployeeCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        this._applicationDbContext = applicationDbContext;
        this._mapper = mapper;
    }
    public async Task<int> Handle(CreateJsonEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee entity = _mapper.Map<Employee>(request);

        _applicationDbContext.Employees.Add(entity);

        return await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}