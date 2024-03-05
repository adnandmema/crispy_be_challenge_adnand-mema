using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.ToDoLists;

namespace ToDoApp.Application.ToDoLists.UpdateToDoList
{
    public class UpdateToDoListCommandValidator : AbstractValidator<UpdateToDoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateToDoListCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
