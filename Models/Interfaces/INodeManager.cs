namespace NodeManagementApp.Models.Interfaces;

public interface INodeManager
{
    void AddNode(INode node);
    void RemoveNode(string nodeId);
    INode? GetNode(string nodeId);
    ICollection<INode> GetNodes();
}