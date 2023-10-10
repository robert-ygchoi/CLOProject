using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Employee
{
    private Employee()
    {

    }

    public Employee(string name, string email, string phoneNumber, string createdAt)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        CreatedAt = createdAt;
    }
    [Key]
    public string Name {  get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string CreatedAt { get; init; }
}

