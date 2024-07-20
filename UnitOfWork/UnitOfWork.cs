using TaskManager.Data;
using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<ToDoItem> _toDoItems;
        private IRepository<Priority> _priorities;
        private IRepository<User> _users;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<ToDoItem> ToDoItems => _toDoItems ??= new GenericRepository<ToDoItem>(_context);
        public IRepository<Priority> Priorities => _priorities ??= new GenericRepository<Priority>(_context);
        public IRepository<User> Users => _users ??= new GenericRepository<User>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
