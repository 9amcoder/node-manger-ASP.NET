using NodeManagementApp.Models.Interfaces;

namespace NodeManagementApp.Models.Classes;

// Used Node directly, as data is constructed on the front-end
public class NodeManager : INodeManager
{
    private readonly List<INode> _nodes;

    public NodeManager()
    {
        _nodes = new List<INode>();
    }

    public void AddNode(INode node)
    {
        if (_nodes.Any(n => n.NodeId == node.NodeId))
        {
            throw new Exception($"Node with id {node.NodeId} already exists");
        }

        _nodes.Add(node);
    }

    public void RemoveNode(string nodeId)
    {
        var node = GetNode(nodeId);
        if (node == null)
        {
            throw new Exception($"No node with id {nodeId} found");
        }

        _nodes.Remove(node);
    }

    public INode? GetNode(string nodeId)
    {
        return _nodes.FirstOrDefault(n => n.NodeId == nodeId);
    }

    public ICollection<INode> GetNodes()
    {
        return _nodes;
    }
}