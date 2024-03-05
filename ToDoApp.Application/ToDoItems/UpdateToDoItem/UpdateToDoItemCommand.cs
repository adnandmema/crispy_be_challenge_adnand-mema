using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoItems.UpdateToDoItem
{
    public record UpdateToDoItemCommand : IRequest
    {
        public int ToDoListId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Note { get; set; }
    }

    public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateToDoItemCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            if (!await _dbContext.ToDoLists.AnyAsync(x => x.Id == request.ToDoListId, cancellationToken))
                throw new EntityNotFoundException(nameof(ToDoList), request.ToDoListId);

            var todoitem = await _dbContext.ToDoItems
                .FirstAsync(x => x.ToDoListId == request.ToDoListId && x.Id == request.Id, cancellationToken)
                ?? throw new EntityNotFoundException(nameof(ToDoItem), request.Id);

            if (_dbContext.ToDoItems.Any(x =>x.Id != request.Id && x.ToDoListId == request.ToDoListId && x.Title == request.Title))
                throw new ValidationException("Title must be unique.");

            todoitem.Title = request.Title;
            todoitem.Note = request.Note;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
