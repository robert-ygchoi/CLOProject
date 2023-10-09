namespace Application.Employees.Queries;

public record EmployeeDto
{
#pragma warning disable CS8618 // AutoMapper ctor
    private EmployeeDto()
#pragma warning restore CS8618 // AutoMapper ctor
    {
    }

    public EmployeeDto(string name, string email, string tel, string joined)
    {
        Name = name;
        Email = email;
        Tel = tel;
        Joined = joined;
    }

    public string Name { get; init; } 
    public string Email { get; init; } 
    public string Tel { get; init; } 
    public string Joined { get; init; }
}
