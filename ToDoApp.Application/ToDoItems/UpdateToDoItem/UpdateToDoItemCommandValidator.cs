using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.ToDoLists.UpdateToDoList;

namespace ToDoApp.Application.ToDoItems.UpdateToDoItem
{
    public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateToDoItemCommandValidator(IApplicationDbContext context)
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
