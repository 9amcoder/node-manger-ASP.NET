using System.Collections.Generic;
using NodeManagementApp.Models;

namespace NodeManagementApp.Services
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