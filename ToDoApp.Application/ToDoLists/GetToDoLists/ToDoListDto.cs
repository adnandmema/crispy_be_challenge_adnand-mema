using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Mapping;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoLists.GetToDoLists
{
    public record ToDoListDto : IMapFrom<ToDoList>
    {
        public int Id { get; init; }
        public string Title { get; set; }
    }
}
