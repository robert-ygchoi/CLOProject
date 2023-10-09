using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Employee
{
    private Employee()
    {

    }

    public Employee(int id, string name, string email, string phoneNumber, string createdAt)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        CreatedAt = createdAt;
    }

    [Key]
    public int Id { get; init; }
    public string Name {  get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string CreatedAt { get; init; }
}

