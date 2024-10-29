using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Markers;
using ToDoApp.Application.Validators;
using ToDoApp.HostedServices;
using ToDoApp.Hubs;
using ToDoApp.Infrastructure.Entities;
using ToDoApp.Infrastructure.Repositories;
using ToDoApp.Middlewares;
using ToDoApp.Persistence;
using ToDoApp.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly));
builder.Services.AddSignalR();

builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddTransient<IValidator<ToDoItem>, ToDoItemValidator>();

builder.Services.AddControllersWithViews();
builder.Services.AddHostedService<ReminderService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NotificationHub>("/notificationHub");

app.Run();
