using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private static readonly List<Employee> Employees =
    [
        new Employee { Id = 1, Name = "Alice", Salary = 60000, Permanent = true, Department = "Sales" },
        new Employee { Id = 2, Name = "Bob", Salary = 72000, Permanent = true, Department = "IT" },
        new Employee { Id = 3, Name = "Charlie", Salary = 54000, Permanent = false, Department = "HR" }
    ];

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Employee>> Get()
    {
        return Ok(Employees);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Employee> Get(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid employee id");
        }

        var employee = Employees.FirstOrDefault(item => item.Id == id);

        if (employee is null)
        {
            return BadRequest("Invalid employee id");
        }

        return Ok(employee);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> Post([FromBody] Employee employee)
    {
        if (string.IsNullOrWhiteSpace(employee.Name))
        {
            return BadRequest("Employee name is required.");
        }

        employee.Id = Employees.Count == 0 ? 1 : Employees.Max(item => item.Id) + 1;
        Employees.Add(employee);

        return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> Put(int id, [FromBody] Employee employee)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid employee id");
        }

        var index = Employees.FindIndex(item => item.Id == id);

        if (index < 0)
        {
            return BadRequest("Invalid employee id");
        }

        employee.Id = id;
        Employees[index] = employee;

        var updatedEmployee = Employees.First(item => item.Id == id);
        return Ok(updatedEmployee);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid employee id");
        }

        var index = Employees.FindIndex(item => item.Id == id);

        if (index < 0)
        {
            return BadRequest("Invalid employee id");
        }

        Employees.RemoveAt(index);
        return NoContent();
    }
}