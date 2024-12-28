namespace MyProject.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}