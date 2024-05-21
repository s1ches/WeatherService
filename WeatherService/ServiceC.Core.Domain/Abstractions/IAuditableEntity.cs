namespace ServiceC.Core.Domain.Abstractions;

public interface IAuditableEntity : IEntity
{
    DateTime CreateDate { get; set; }
    
    DateTime EditDate { get; set; }
}