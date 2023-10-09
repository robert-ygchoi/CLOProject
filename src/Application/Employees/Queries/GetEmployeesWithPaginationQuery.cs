using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Employees.Queries;

public record GetEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetEmployeesWithPaginationQueryHandler : IRequestHandler<GetEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetEmployeesWithPaginationQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        this._applicationDbContext = applicationDbContext;
        this._mapper = mapper;
    }

    public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Employees
            .OrderBy(e => e.Name)
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
