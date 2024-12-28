using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Domain.Common;

public abstract class Entity
{
    protected readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(Guid id) => Id = id;

    protected Entity()
    {
    }

    public Guid Id { get; init; }

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();

        _domainEvents.Clear();

        return copy;
    }
}