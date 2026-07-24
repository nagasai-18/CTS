namespace WebApiDemo.Models;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Salary { get; set; }

    public bool Permanent { get; set; }

    public Department Department { get; set; } = new();

    public List<Skill> Skills { get; set; } = [];

    public DateTime DateOfBirth { get; set; }
}