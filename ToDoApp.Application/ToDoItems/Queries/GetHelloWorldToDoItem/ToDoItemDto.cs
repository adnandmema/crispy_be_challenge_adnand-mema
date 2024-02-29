using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Common.Mapping;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoItems.Queries.GetHelloWorldToDoItem
{
    public class ToDoItemDto : IMapFrom<ToDoItem>
    {
        public string Title { get; set; }
    }
}
