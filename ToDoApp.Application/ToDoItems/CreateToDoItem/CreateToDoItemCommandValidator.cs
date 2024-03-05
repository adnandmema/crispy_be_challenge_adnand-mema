using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.ToDoLists.CreateToDoList;

namespace ToDoApp.Application.ToDoItems.CreateToDoItem
{
    public class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateToDoItemCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(v => v.Note)
                .MaximumLength(500);
        }
    }
}
