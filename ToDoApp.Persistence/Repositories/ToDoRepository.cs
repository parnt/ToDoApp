using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Infrastructure.Entities;
using ToDoApp.Infrastructure.Repositories;

namespace ToDoApp.Persistence.Repositories;

public class ToDoRepository : IToDoRepository
{
    private readonly AppDbContext _context;

    public ToDoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ToDoItem item, CancellationToken cancellationToken)
    {
        if (item is null) throw new ArgumentException("Argument cannot be null.", nameof(item));

        await _context.TodoItems.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(ToDoItem item, CancellationToken cancellationToken)
    {
        if (item is null) throw new ArgumentException("Argument cannot be null.", nameof(item));

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ToDoItem>> GetItemsAsync(DateTime dateTime, CancellationToken cancellationToken)
    {
        Expression<Func<ToDoItem, bool>> predicate = x =>
            x.DueDate >= dateTime.Date && x.DueDate < dateTime.Date.AddDays(7);

        var items = await GetItemsAsync(predicate, cancellationToken);

        return items;
    }

    public async Task<IEnumerable<ToDoItem>> GetUpcomingAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var truncatedNow = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
        var targetTime = truncatedNow.AddHours(1);

        Expression<Func<ToDoItem, bool>> predicate = x =>
            !x.IsCompleted && 
            x.DueDate >= targetTime && x.DueDate < targetTime.AddMinutes(1);

        var items = await GetItemsAsync(predicate, cancellationToken);

        return items;
    }

    public async Task<ToDoItem> GetItemAsync(int id, CancellationToken cancellationToken)
    {
        var item = await RetrieveItemAsync(id, cancellationToken);

        return item;
    }

    public async Task DeleteItemAsync(int id, CancellationToken cancellationToken)
    {
        _ = await RetrieveItemAsync(id, cancellationToken, item => item.IsDeleted = true);
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<IEnumerable<ToDoItem>> GetItemsAsync(Expression<Func<ToDoItem, bool>> predicate, CancellationToken cancellationToken)
    {
        var items = await _context.TodoItems
            .Where(predicate)
            .AsNoTracking()
            .OrderBy(x => x.DueDate)
            .ToListAsync(cancellationToken);

        return items;
    }

    private async Task<ToDoItem> RetrieveItemAsync(int id, CancellationToken cancellationToken, Action<ToDoItem>? action = null)
    {
        if (id <= 0) throw new ArgumentException("Argument cannot be less or equal than 0.", nameof(id));

        var item = await _context.TodoItems.FindAsync([id], cancellationToken);
        
        if (item is null) throw new KeyNotFoundException("Id is incorrect.");
        
        action?.Invoke(item);

        return item;
    }
}