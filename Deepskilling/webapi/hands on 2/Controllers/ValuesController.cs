using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    private static readonly List<string> Values = ["value1", "value2"];

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<string>> Get()
    {
        return Ok(Values);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<string> Get(int id)
    {
        if (id < 0 || id >= Values.Count)
        {
            return NotFound();
        }

        return Ok(Values[id]);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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