using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ToDoList> ToDoLists { get; }
        DbSet<ToDoItem> ToDoItems { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
