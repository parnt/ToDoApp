using ToDoApp.Infrastructure.Entities;

namespace ToDoApp.Infrastructure.Repositories;

public interface IToDoRepository
{
    Task AddAsync(ToDoItem item, CancellationToken cancellationToken);

    Task SaveChangesAsync(ToDoItem item, CancellationToken cancellationToken);

    Task<IEnumerable<ToDoItem>> GetItemsAsync(DateTime dateTime, CancellationToken cancellationToken);
    
    Task<IEnumerable<ToDoItem>> GetUpcomingAsync(CancellationToken cancellationToken);

    Task<ToDoItem> GetItemAsync(int id, CancellationToken cancellationToken);

    Task DeleteItemAsync(int id, CancellationToken cancellationToken);
}