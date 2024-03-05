using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoItems.DeleteToDoItem
{
    public record DeleteToDoItemCommand : IRequest
    {
        public int ToDoListId { get; init; }
        public int Id { get; init; }
    }

    public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteToDoItemCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            if (!await _dbContext.ToDoLists.AnyAsync(x => x.Id == request.ToDoListId, cancellationToken))
                throw new EntityNotFoundException(nameof(ToDoList), request.ToDoListId);

            var todoitem = await _dbContext.ToDoItems.FirstAsync(x => x.Id == request.Id && x.ToDoListId == request.ToDoListId, cancellationToken)
                ?? throw new EntityNotFoundException(nameof(ToDoItem), request.Id);

            _dbContext.ToDoItems.Remove(todoitem);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
