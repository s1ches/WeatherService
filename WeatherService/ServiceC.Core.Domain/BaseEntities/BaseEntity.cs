using ServiceC.Core.Domain.Abstractions;

namespace ServiceC.Core.Domain.BaseEntities;

public class BaseEntity : IEntity
{
    public Guid Id { get; set; }
}