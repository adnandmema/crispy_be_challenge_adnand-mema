using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;

namespace ToDoApp.SampleData.Samples
{
    public static class SampleToDoLists
    {
        public static List<ToDoList> GetSampleToDoLists() 
        {
            return new List<ToDoList>
            {
                new ToDoList()
                {
                    Title = "Work",
                    Items = 
                    {
                        new ToDoItem() {Title = "ASAP", Note = "Database setup"},
                        new ToDoItem() {Title = "High Priority", Note = "CI/CD"},
                        new ToDoItem() {Title = "#1235 Ticket", Note = "Bug fixing"},
                    }
                },
                new ToDoList() 
                {
                    Title = "After Work",
                    Items =
                    {
                        new ToDoItem() {Title = "ASAP", Note = "Pay the bills"},
                        new ToDoItem() {Title = "Wash the car"},
                    }
                }
            };
        }
    }
}
