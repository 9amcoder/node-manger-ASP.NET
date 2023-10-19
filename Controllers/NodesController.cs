using Microsoft.AspNetCore.Mvc;
using NodeManagementApp.Models.Classes;
using NodeManagementApp.Services;

namespace NodeManagementApp.Controllers;

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
    public async Task<IActionResult> List()
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
    public async Task<IActionResult> Create(Node node) // create a Node object in the front end then just deposit it
    {
        if (!ModelState.IsValid || string.IsNullOrEmpty(node.NodeId))
        {
            return BadRequest();
        }

        await _service.CreateAsync(node);
        
        return CreatedAtRoute("GetNode", new { id = node.NodeId }, node);
    }

    [HttpPut("{id}", Name = "UpdateNode")]
    public async Task<IActionResult> Update(string id, Node node)
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

    [HttpPut("{nodeId}", Name = "OnlineNode")]
    public async Task<IActionResult> Online(string nodeId, bool online = true)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var node = await _service.FindAsync(nodeId);
        if (node == null)
        {
            return BadRequest($"{nodeId} does NOT exists.");
        }

        if (online)
        {
            node.SetOnline();
        }
        else
        {
            node.SetOffline();
        }
        
        await _service.UpdateAsync(nodeId, node);
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
        
        // if id is still in the db, that means failed
        var exists = await _service.FindAsync(id);
        if (exists == null)
        {
            return NoContent();
        }

        return BadRequest($"{id} could NOT be deleted");
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
    public async Task<IActionResult> UpdateThresholds(string id, Thresholds thresholds)
    {
        var node = await _service.FindAsync(id);
        
        if (node == null)
        {
            return NotFound();
        }
        
        var updated = await _service.UpdateThresholdsAsync(id, thresholds);

        return updated ? NoContent() : BadRequest($"Node id {id} thresholds can NOT be updated.");
    }

    [HttpGet("alarms", Name = "GetAlarms")]
    public async Task<IActionResult> ListAlarms()
    {
        var alarms = await _service.GetAlarmsAsync();
        
        return Ok(alarms);
    }
}