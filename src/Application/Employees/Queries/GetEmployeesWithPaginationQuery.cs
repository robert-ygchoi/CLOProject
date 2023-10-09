﻿using Application.Common.Models;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Employees.Queries;

public record GetEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetEmployeesWithPaginationQueryHandler : IRequestHandler<GetEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .OrderBy(e => e.Name)
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}