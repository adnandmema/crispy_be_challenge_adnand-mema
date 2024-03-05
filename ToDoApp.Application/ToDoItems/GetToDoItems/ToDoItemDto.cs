using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Mapping;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoItems.GetToDoItems
{
    public record ToDoItemDto : IMapFrom<ToDoItem>
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public string? Note { get; set; }
    }
}
