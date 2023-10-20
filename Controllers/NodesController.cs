using Microsoft.AspNetCore.Mvc;
using NodeManagementApp.Models.Classes;
using NodeManagementApp.Services;
using NodeManagementApp.Models.Interfaces;

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
    public async Task<IActionResult> Create(Node node)
    {
        if (!IsValidNode(node))
        {
            return BadRequest();
        }

        await _service.CreateAsync(node);

        // Generate alarms based on the thresholds and current values
        GenerateAlarms(node);
        
        return CreatedAtRoute("GetNode", new { id = node.NodeId }, node);
    }

    private void GenerateAlarms(Node node)
    {
        if (node.UploadUtilization > node.Thresholds.MaxUploadUtilization)
        {
            node.Alarms.Add(new Alarm { NodeId = node.NodeId, Metric = "UploadUtilization", Value = node.UploadUtilization });
        }
        if (node.DownloadUtilization > node.Thresholds.MaxDownloadUtilization)
        {
            node.Alarms.Add(new Alarm { NodeId = node.NodeId, Metric = "DownloadUtilization", Value = node.DownloadUtilization });
        }
        if (node.ErrorRate > node.Thresholds.MaxErrorRate)
        {
            node.Alarms.Add(new Alarm { NodeId = node.NodeId, Metric = "ErrorRate", Value = node.ErrorRate });
        }
        if (node.ConnectedClients > node.Thresholds.MaxConnectedClients)
        {
            node.Alarms.Add(new Alarm { NodeId = node.NodeId, Metric = "ConnectedClients", Value = node.ConnectedClients });
        }
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

    [HttpPut("{nodeId}/setOnline", Name = "OnlineNode")]
    public async Task<IActionResult> Online(string nodeId, Node onlineNode)
    {
        if (!IsValidNode(onlineNode))
        {
            return BadRequest();
        }

        var node = await _service.FindAsync(nodeId);
        if (node == null)
        {
            return NotFound();
        }

        node.IsOnline = onlineNode.IsOnline;

        await _service.UpdateAsync(nodeId, node);
        return NoContent();
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

    private bool IsValidNode(Node node)
    {
        return ModelState.IsValid && !string.IsNullOrEmpty(node.NodeId);
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