namespace Advertising.Domain.Uow
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
