namespace Templify.Domain.Common;

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
}

public interface IEntity
{
    int Id { get; set; }
}
