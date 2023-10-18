using Microsoft.AspNetCore.Mvc;
using NodeManagementApp.Services;
using NodeManagementApp.Models;
using Serilog;

[ApiController]
[Route("api/nodes")]
public class NodesController : ControllerBase
{
    private readonly NodesService _service;

    public NodesController(NodesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var nodes = await _service.ReadAsync();
        return Ok(nodes);
    }

    [HttpGet("{id}", Name = "GetNode")]
    public async Task<IActionResult> Get(string id)
    {
        var node = await _service.FindAsync(id);
        if (node == null)
        {
            return NotFound();
        }
        return Ok(node);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Node node)
    {
        if (!ModelState.IsValid || string.IsNullOrEmpty(node.NodeId))
        {
            Log.Error("Invalid Node object");
            return BadRequest();
        }

        await _service.CreateAsync(node);
        Log.Information("Created Node with id: {NodeId}", node.NodeId);
        
        return CreatedAtRoute("GetNode", new { id = node.NodeId }, node);
    }

    [HttpPut("{id}", Name = "UpdateNode")]
    public async Task<IActionResult> Update(string id, [FromBody] Node node)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingNode = await _service.FindAsync(id);
        if (existingNode == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(id, node);
        return NoContent();
    }


    private bool IsValid(Node node)
    {
        //TODO: Add validation logic here.
        return ModelState.IsValid;
    }

    [HttpDelete("{id}", Name = "DeleteNode")]
    public async Task<IActionResult> Delete(string id)
    {
        var node = await _service.FindAsync(id);
        if (node == null)
        {
            return NotFound();
        }

        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/telemetry", Name = "GetNodeTelemetry")]
    public async Task<IActionResult> GetTelemetry(string id)
    {
        var telemetry = await _service.GetTelemetryAsync(id);
        if (telemetry == null)
        {
            return NotFound();
        }
        return Ok(telemetry);
    }


    [HttpPut("{id}/thresholds", Name = "UpdateNodeThresholds")]
    public async Task<IActionResult> UpdateThresholds(string id, [FromBody] Thresholds thresholds)
    {
        var existingNode = await _service.FindAsync(id);
        if (existingNode == null)
        {
            return NotFound();
        }

        await _service.UpdateThresholdsAsync(id, thresholds);
        return NoContent();
    }

    [HttpGet("alarms", Name = "GetAlarms")]
    public async Task<IActionResult> GetAlarms()
    {
        var alarms = await _service.GetAlarmsAsync();
        return Ok(alarms);
    }
}
