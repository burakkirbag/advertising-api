namespace Advertising.Domain.Entities
{
    public abstract class EntityBase : IEntity
    {
        public virtual int Id { get; set; }

        public override string ToString() => $"[{GetType().Name}_{Id}]";
    }
}
