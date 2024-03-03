using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Models;
using ToDoApp.Application.ToDoItems.CreateToDoItem;
using ToDoApp.Application.ToDoItems.DeleteToDoItem;
using ToDoApp.Application.ToDoItems.GetToDoItems;
using ToDoApp.Application.ToDoItems.UpdateToDoItem;
using ToDoApp.Application.ToDoLists.CreateToDoList;
using ToDoApp.Application.ToDoLists.DeleteToDoList;
using ToDoApp.Application.ToDoLists.GetToDoLists;
using ToDoApp.Application.ToDoLists.UpdateToDoList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ToDoApp.API.API.V1
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region ToDoList

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> CreateToDoList([FromBody] CreateToDoListCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ToDoListDto>>> GetToDoLists([FromQuery] GetToDoListsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoList(int id, [FromBody] UpdateToDoListCommand command)
        {
            if (id != command.Id) 
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoList(int id, [FromBody] DeleteToDoListCommand command)
        {
            if (id != command.Id) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion ToDoList

        #region ToDoItem

        [Authorize]
        [HttpPost("{listId}/todoitem")]
        public async Task<ActionResult<int>> CreateToDoItem(int listId, [FromBody] CreateToDoItemCommand command)
        {
            if (listId != command.ToDoListId)
                return BadRequest();

            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpGet("{listId}/todoitem")]
        public async Task<ActionResult<PaginatedList<ToDoItemDto>>> GetToDoItems(int listId, [FromQuery] GetToDoItemsQuery query)
        {
            if (listId != query.ListId)
                return BadRequest();

            return Ok(await _mediator.Send(query));
        }

        [Authorize]
        [HttpPut("{listId}/todoitem/{toDoItemId}")]
        public async Task<IActionResult> UpdateToDoItem(int listId, int toDoItemId, [FromBody] UpdateToDoItemCommand command)
        {
            if (listId != command.ToDoListId || toDoItemId != command.Id) 
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{listId}/todoitem/{toDoItemId}")]
        public async Task<IActionResult> DeleteToDoItem(int listId, int toDoItemId, [FromBody] DeleteToDoItemCommand command)
        {
            if (listId != command.ToDoListId || toDoItemId != command.Id)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion ToDoItem
    }
}
