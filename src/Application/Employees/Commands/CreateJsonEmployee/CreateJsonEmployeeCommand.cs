using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
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
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateJsonEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }
    public async Task<int> Handle(CreateJsonEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee entity = _mapper.Map<Employee>(request);

        _context.Employees.Add(entity);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}