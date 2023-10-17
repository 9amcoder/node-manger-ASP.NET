using Microsoft.AspNetCore.Mvc;
using NodeManagementApp.Services;
using NodeManagementApp.Models;

[ApiController]
[Route("api/nodes")]
public class NodesController : Controller
{
    private readonly NodesService _service;

    public NodesController(NodesService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get() =>
        Ok(_service.Read());

    [HttpGet("{id:length(24)}", Name = "GetNode")]
    public IActionResult Get(string id)
    {
        var node = _service.Find(id);

        if (node == null)
            return NotFound();

        return Ok(node);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Node node)
    {
        if (!ModelState.IsValid || string.IsNullOrEmpty(node.NodeId))
        {
            return BadRequest();
        }

        _service.Create(node);

        return CreatedAtRoute("GetNode", new { id = node.NodeId }, node);
    }

    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, [FromBody] Node node)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingNode = _service.Find(id);

        if (existingNode == null)
            return NotFound();

        _service.Update(id, node);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var node = _service.Find(id);

        if (node == null)
            return NotFound();

        _service.Delete(id);
        return NoContent();
    }
}