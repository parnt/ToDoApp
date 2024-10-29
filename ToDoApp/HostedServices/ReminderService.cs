using MediatR;
using Microsoft.AspNetCore.SignalR;
using ToDoApp.Application.Queries;
using ToDoApp.Hubs;

namespace ToDoApp.HostedServices;

public class ReminderService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IServiceScopeFactory _scopeFactory;

    public ReminderService(IHubContext<NotificationHub> hubContext, IServiceScopeFactory scopeFactory)
    {
        _hubContext = hubContext;
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(CheckDueDates!, null, TimeSpan.Zero, TimeSpan.FromSeconds(59));
        return Task.CompletedTask;
    }

    private async void CheckDueDates(object state)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var model = new GetUpcomingToDoItems();
        
        var result = (await mediator.Send(model)).ToList();

        if (result.Count == 0) return;
        
        var message = "The following tasks are due within an hour: <br />" + 
                      string.Join(", <br />", result.Select(t => $"{t.DueDate} - {t.Title}"));
            
        await _hubContext.Clients.All.SendAsync("NotifyUpcomingTasks", message);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}