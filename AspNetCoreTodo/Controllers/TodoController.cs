using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace AspNetCoreTodo.Controllers;

[Authorize]
public class TodoController : Controller
{
    private readonly ITodoItemService _todoItemService;
    private readonly UserManager<IdentityUser> _userManager;

    public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
    {
        _todoItemService = todoItemService;
        _userManager = userManager;

    }
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser == null)
            return Challenge();

        var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);

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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(TodoItem newItem)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }

        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null) return Challenge();

        var successful = await _todoItemService
        .AddItemAsync(newItem, currentUser);

        if (!successful)
        {
            return BadRequest("Could not add item.");
        }
        return RedirectToAction("Index");
    }
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkDone(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }
        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser == null) return Challenge();

        var successful = await _todoItemService.MarkDoneAsync(id, currentUser);

        if (!successful)
        {
            return BadRequest("Could not mark item as done.");
        }
        return RedirectToAction("Index");
    }

}
