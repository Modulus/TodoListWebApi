using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListWebApi.Models;

namespace TodoListWebApi.Mappers
{
    public class TodoMapper
    {
        public static TodoItem Map(MongoTodoItem mongoItem)
        {
            var item = new TodoItem()
            {
                Id = mongoItem.Id,
                Text = mongoItem.Description,
                Done = mongoItem.Done
            };

            return item;
        }
    }
}