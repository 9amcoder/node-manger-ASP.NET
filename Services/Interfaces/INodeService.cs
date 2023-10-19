using NodeManagementApp.Models.Classes;

namespace NodeManagementApp.Services.Interfaces
{
    public interface INodeService
    {
        Node CreateNode(Node newNode);
        void DeleteNode(int nodeId);
        Node UpdateNode(Node updatedNode);
        Node GetNodeById(int nodeId);
        IEnumerable<Node> GetAllNodes();
    }
}