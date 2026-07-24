using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers;

public record Employee(int Id, string Name, string Department, bool IsActive);

public record EmployeeCreateRequest(string Name, string Department, bool IsActive);

[ApiController]
[Route("api/Emp")]
public class EmployeeController : ControllerBase
{
    private static readonly List<Employee> Employees =
    [
        new(1, "Alice", "Sales", true),
        new(2, "Bob", "IT", true),
        new(3, "Charlie", "HR", false)
    ];

    [HttpGet(Name = "GetEmployees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Employee>> GetEmployees()
    {
        return Ok(Employees);
    }

    [HttpGet("{id:int}", Name = "GetEmployeeById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Employee> GetEmployeeById(int id)
    {
        var employee = Employees.FirstOrDefault(item => item.Id == id);

        if (employee is null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpGet("active", Name = "GetActiveEmployees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Employee>> GetActiveEmployees()
    {
        return Ok(Employees.Where(item => item.IsActive));
    }

    [HttpGet("by-department/{department}", Name = "GetEmployeesByDepartment")]
    [ActionName("ByDepartment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Employee>> GetEmployeesByDepartment(string department)
    {
        return Ok(Employees.Where(item => string.Equals(item.Department, department, StringComparison.OrdinalIgnoreCase)));
    }

    [HttpPost(Name = "CreateEmployee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> CreateEmployee([FromBody] EmployeeCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Department))
        {
            return BadRequest("Name and Department are required.");
        }

        var employee = new Employee(Employees.Count == 0 ? 1 : Employees.Max(item => item.Id) + 1, request.Name, request.Department, request.IsActive);
        Employees.Add(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }
}