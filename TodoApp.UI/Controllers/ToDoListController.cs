using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using ToDoApp.UI.Models;
using ToDoApp.UI.Services;

namespace ToDoApp.UI.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly ToDoAppService _toDoAppService;

        public ToDoListController(ToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _toDoAppService.GetDoDoLists());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateToDoListDto createToDoListDto)
        {
            try
            {
                var result = await _toDoAppService.CreateToDoList(createToDoListDto);
                if (result)
                    return RedirectToAction(nameof(Index));

                throw new Exception("There was an error while creating ToDo List");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _toDoAppService.DeleteToDoList(id);
                if (result)
                    return RedirectToAction("Index", "ToDoList");

                throw new Exception("There was an error while deleting ToDo List");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
