using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Common;

namespace ToDoApp.Domain.Entities
{
    public class ToDoList : FullEntity
    {
        public string Title { get; set; }
        public IList<ToDoItem> Items { get; private set; } = new List<ToDoItem>();
    }
}
