using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ToDoItem> ToDoItems { get; }
        IRepository<Priority> Priorities { get; }
        IRepository<User> Users { get; }
        Task<int> CompleteAsync();
    }
}
