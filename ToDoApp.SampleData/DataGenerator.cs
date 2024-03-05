using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ToDoApp.Domain.Entities;
using ToDoApp.SampleData.Samples;

namespace ToDoApp.SampleData
{
    public static class DataGenerator
    {
        public static List<ToDoList> GenerateBaseEntities()
        {
            var partners = SampleToDoLists.GetSampleToDoLists();

            return partners;
        }
    }
}
