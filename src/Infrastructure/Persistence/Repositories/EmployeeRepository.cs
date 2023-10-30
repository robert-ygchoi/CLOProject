using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        this._context = context;
    }

    public DbSet<Employee> Employees => _context.Employees;

    public async Task<int> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        var query = from e in _context.Employees
                    where e.Name == employee.Name
                    select e;

        if (await query.AnyAsync(cancellationToken))
            throw new DuplicateKeyException(employee.Name);

        _context.Employees.Add(employee);

        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Employee?> GetEmployeeByNameAsync(string name, CancellationToken cancellationToken)
    {
        var query = from employee in this._context.Employees.AsNoTracking()
                    where employee.Name == name
                    select employee;

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
