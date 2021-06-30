using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Node.Domain.AggregatesModel.NodeAggregates
{
    public interface INodesRepository : IRepository<Nodes> 
    {
        Nodes Add(Nodes node);
        void Update(string IdentityNode, Nodes node);
        bool Delete(string IdentityNode);
        bool AddUserNode(string IdendityUser, string IdendityNode);
    }
}
