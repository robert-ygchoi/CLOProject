using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IEmployeeRepository
{
    public DbSet<Employee> Employees { get; }
    public Task<int> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
    public Task<Employee?> GetEmployeeByNameAsync(string name, CancellationToken cancellationToken);
}
