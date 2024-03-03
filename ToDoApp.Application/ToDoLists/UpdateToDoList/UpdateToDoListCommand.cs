using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoLists.UpdateToDoList
{
    public record UpdateToDoListCommand : IRequest
    {
        public int Id { get; init; }
        public string Title { get; init; } = null!;
    }

    public class UpdateToDoListCommandHandler : IRequestHandler<UpdateToDoListCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateToDoListCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateToDoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.ToDoLists
                .FindAsync(new object[] { request.Id }, cancellationToken)
                ?? throw new EntityNotFoundException(nameof(ToDoList), request.Id);

            if (await _dbContext.ToDoLists.AnyAsync(x => x.Id != request.Id && x.Title == request.Title, cancellationToken))
                throw new ValidationException("Title must be unique.");

            entity.Title = request.Title;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
