namespace CodeHelp.Repository.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetCurrentUnitOfWork();
    }
}