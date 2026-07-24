using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private static readonly List<Employee> Employees =
    [
        new() { Id = 1, Name = "Alice", Salary = 60000, Permanent = true, Department = "Sales" },
        new() { Id = 2, Name = "Bob", Salary = 72000, Permanent = true, Department = "IT" },
        new() { Id = 3, Name = "Charlie", Salary = 54000, Permanent = false, Department = "HR" }
    ];

    [HttpGet]
    [Authorize(Roles = "Admin,POC")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult<List<Employee>> Get()
    {
        return Ok(Employees);
    }

    [HttpGet("poc-only")]
    [Authorize(Roles = "POC")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ActionResult<List<Employee>> GetForPocOnly()
    {
        return Ok(Employees);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,POC")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
}