using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.ToDoItems.Queries.GetHelloWorldToDoItem;

namespace ToDoApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<ToDoItemDto> GetHelloWorldToDoItem([FromQuery] GetHelloWorldToDoItemQuery query)
        {
            return _mediator.Send(query);
        }
    }
}
