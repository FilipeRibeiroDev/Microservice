using Domain.Seedwork;
using Domain.Exceptions;
using System;

namespace Node.Domain.AggregatesModel.NodeAggregates
{
    public class Nodes: Entity, IAggregateRoot
    {
        public string IdentityNode { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime Created { get; private set; }
        public string Code { get; private set; }

        public Nodes(string name, string descripton, string code, string identity = null)
        {
            IdentityNode = string.IsNullOrWhiteSpace(identity) ? Guid.NewGuid().ToString() : identity;
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new DomainException("Invalid Name. Please, enter a valid user.");
            Description = !string.IsNullOrWhiteSpace(descripton) ? descripton : throw new DomainException("Invalide Description. Please, enter a valid description.");
            Code = !string.IsNullOrWhiteSpace(code) ? code : throw new DomainException("Invalid Code. Please, enter a valid code.");
            Created = DateTime.UtcNow;
        }

    }
}
