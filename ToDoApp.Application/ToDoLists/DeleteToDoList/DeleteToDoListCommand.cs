using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoLists.DeleteToDoList
{
    public record DeleteToDoListCommand : IRequest
    {
        public int Id { get; init; }
    }

    public class DeleteToDoListCommandHandler : IRequestHandler<DeleteToDoListCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteToDoListCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.ToDoLists
                .FindAsync(new object[] { request.Id }, cancellationToken)
                ?? throw new EntityNotFoundException(nameof(ToDoList), request.Id);

            _dbContext.ToDoLists.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
