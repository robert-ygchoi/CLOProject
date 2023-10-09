namespace Application.Employees.Queries;

public record EmployeeContactDto
{
#pragma warning disable CS8618 // AutoMapper ctor
    private EmployeeContactDto()
#pragma warning restore CS8618 // AutoMapper ctor
    {

    }

    public EmployeeContactDto(string name, string tel, string email)
    {
        Name = name;
        Tel = tel;
        Email = email;
    }

    public string Name { get; init; }
    public string Tel { get; init; }
    public string Email { get; init; }
}
