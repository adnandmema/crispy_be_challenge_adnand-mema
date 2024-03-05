using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoLists.CreateToDoList
{
    public record CreateToDoListCommand : IRequest<int>
    {
        public string Title { get; init; } = null!;
    }

    public class CreateToDoListCommandHandler : IRequestHandler<CreateToDoListCommand, int>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateToDoListCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateToDoListCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.ToDoLists.AnyAsync(x => x.Title == request.Title, cancellationToken))
                throw new ValidationException("Title must be unique.");

            var todolist = new ToDoList()
            { 
                Title = request.Title 
            };

            _dbContext.ToDoLists.Add(todolist);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return todolist.Id;
        }
    }
}
