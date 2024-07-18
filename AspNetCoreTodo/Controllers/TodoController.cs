using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCoreTodo.Controllers
{
    
public class TodoController : Controller
{
private readonly ITodoItemService _todoItemService;
public TodoController(ITodoItemService todoItemService)
{
_todoItemService = todoItemService;
}
public async Task<IActionResult> Index()
{
    var items = await _todoItemService.GetIncompleteItemsAsync();
    
// Get to-do items from database
// Put items into a model
// Pass the view to a model and render
  var model = new TodoViewModel
        {
            Items = items // Initialize list of to-do items
        };

        // Render view using the model, passing the populated view model to the view
        return View(model);
}
}
}