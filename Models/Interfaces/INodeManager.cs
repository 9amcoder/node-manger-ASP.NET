using System.Collections.Generic;
namespace NodeManagementApp.Models
{
    public interface INodeManager
    {
        void AddNode(INode node);
        void RemoveNode(string nodeId);
        INode? GetNode(string nodeId);
        ICollection<INode> GetNodes();
    }
}