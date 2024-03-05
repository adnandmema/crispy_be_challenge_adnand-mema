using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Common;

namespace ToDoApp.Domain.Entities
{
    public class ToDoItem : FullEntity
    {
        public int ToDoListId { get; set; }
        public string Title { get; set; }
        public string? Note { get; set; }
        public ToDoList ToDoList { get; set; } = null!;
    }
}
