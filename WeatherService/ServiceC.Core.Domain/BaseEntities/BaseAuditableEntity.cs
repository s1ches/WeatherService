using ServiceC.Core.Domain.Abstractions;

namespace ServiceC.Core.Domain.BaseEntities;

public class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public DateTime CreateDate { get; set; }
    
    public DateTime EditDate { get; set; }
}