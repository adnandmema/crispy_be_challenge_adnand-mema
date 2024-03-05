using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Application.ToDoLists.CreateToDoList
{
    public class CreateToDoListCommandValidator : AbstractValidator<CreateToDoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateToDoListCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
