using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApiDemo.Filters;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
[CustomAuthFilter]
public class EmployeeController : ControllerBase
{
    private readonly List<Employee> employees = GetStandardEmployeeList();

    private static List<Employee> GetStandardEmployeeList()
    {
        return
        [
            new Employee
            {
                Id = 1,
                Name = "Alice",
                Salary = 60000,
                Permanent = true,
                Department = new Department { Id = 1, Name = "Sales" },
                Skills = [new Skill { Id = 1, Name = "Communication" }, new Skill { Id = 2, Name = "Negotiation" }],
                DateOfBirth = new DateTime(1995, 4, 10)
            },
            new Employee
            {
                Id = 2,
                Name = "Bob",
                Salary = 75000,
                Permanent = true,
                Department = new Department { Id = 2, Name = "IT" },
                Skills = [new Skill { Id = 3, Name = "C#" }, new Skill { Id = 4, Name = "SQL" }],
                DateOfBirth = new DateTime(1992, 11, 2)
            }
        ];
    }

    [HttpGet("standard")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<Employee>> GetStandard()
    {
        throw new InvalidOperationException("Custom exception filter demo exception.");
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Employee>> Get()
    {
        return Ok(GetStandardEmployeeList());
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Employee> Get(int id)
    {
        var employee = GetStandardEmployeeList().FirstOrDefault(item => item.Id == id);

        if (employee is null)
        {
            return NotFound();
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

        employee.Id = employees.Count == 0 ? 1 : employees.Max(item => item.Id) + 1;
        employees.Add(employee);
        return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(int id, [FromBody] Employee employee)
    {
        var index = employees.FindIndex(item => item.Id == id);

        if (index < 0)
        {
            return NotFound();
        }

        employee.Id = id;
        employees[index] = employee;
        return NoContent();
    }
}