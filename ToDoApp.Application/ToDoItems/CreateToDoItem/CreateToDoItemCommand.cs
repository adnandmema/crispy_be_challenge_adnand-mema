using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoItems.CreateToDoItem
{
    public record CreateToDoItemCommand : IRequest<int>
    {
        public int ToDoListId { get; set; }
        public string Title { get; set; } = null!;
        public string? Note { get; set; }
    }

    public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, int>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateToDoItemCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            if (!await _dbContext.ToDoLists.AnyAsync(x => x.Id == request.ToDoListId, cancellationToken))
                throw new EntityNotFoundException(nameof(ToDoList), request.ToDoListId);

            if (await _dbContext.ToDoItems.AnyAsync(x => x.ToDoListId == request.ToDoListId && x.Title == request.Title, cancellationToken)) 
                throw new ValidationException("Title must be unique.");

            var todoitem = new ToDoItem()
            {
                ToDoListId = request.ToDoListId,
                Title = request.Title,
                Note = request.Note
            };

            _dbContext.ToDoItems.Add(todoitem);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return todoitem.Id;
        }
    }
}
