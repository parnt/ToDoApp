using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Commands;
using ToDoApp.Application.Commands.Handlers;
using ToDoApp.Application.Model;
using ToDoApp.Application.Queries;
using ToDoApp.Helpers;
using ToDoApp.Models;

namespace ToDoApp.Controllers;

public class HomeController : Controller
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddToDoItemAsync(ToDoItemDto item, CancellationToken cancellationToken)
    {
        var model = new AddToDoItem(item.Title, item.Description, item.DueDate);
        
        await _mediator.Send(model, cancellationToken);
        
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateToDoItemAsync(ToDoItemEntityDto item, CancellationToken cancellationToken)
    {
        var model = new UpdateToDoItem(item.Id, item.Title, item.Description, item.DueDate, item.IsCompleted);
        
        await _mediator.Send(model, cancellationToken);
        
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteToDoItemAsync(int id, CancellationToken cancellationToken)
    {
        var model = new DeleteToDoItem(id);
        
        await _mediator.Send(model, cancellationToken);
        
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllToDoItemsAsync(DateTime? startDate, CancellationToken cancellationToken)
    {
        var date = DateTimeHelper.GetMondayDate(startDate);
        
        var model = new GetAllToDoItems(date);
        
        var result = await _mediator.Send(model, cancellationToken);
        
        return Json(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var model = new GetToDoItem(id);
        
        var result = await _mediator.Send(model, cancellationToken);
        
        return PartialView("Partials/EditToDoModal", result);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}