using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    private static readonly List<string> Values = ["value1", "value2"];

    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return Ok(Values);
    }

    [HttpGet("{id:int}")]
    public ActionResult<string> Get(int id)
    {
        if (id < 0 || id >= Values.Count)
        {
            return NotFound();
        }

        return Ok(Values[id]);
    }

    [HttpPost]
    public ActionResult<string> Post([FromBody] string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return BadRequest("A value must be supplied.");
        }

        Values.Add(value);
        return CreatedAtAction(nameof(Get), new { id = Values.Count - 1 }, value);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return BadRequest("A value must be supplied.");
        }

        if (id < 0 || id >= Values.Count)
        {
            return NotFound();
        }

        Values[id] = value;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (id < 0 || id >= Values.Count)
        {
            return NotFound();
        }

        Values.RemoveAt(id);
        return NoContent();
    }
}