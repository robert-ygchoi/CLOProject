using Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Employees.Queries;

public record GetEmployeeContactQuery(string Name) : IRequest<EmployeeContactDto?>;

public class GetEmployeeContactQueryHandler : IRequestHandler<GetEmployeeContactQuery, EmployeeContactDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeContactQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }
    public async Task<EmployeeContactDto?> Handle(GetEmployeeContactQuery request, CancellationToken cancellationToken)
    {
        var query = from employee in this._context.Employees.AsNoTracking()
                     where employee.Name == request.Name
                     select employee;

        var entity = await query.FirstOrDefaultAsync(cancellationToken);

        // NOTE; null is not found
        if (entity is null)
            return null;

        return _mapper.Map<EmployeeContactDto>(entity);
    }
}


