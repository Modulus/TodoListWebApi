using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListWebApi.Mappers;
using TodoListWebApi.Models;
using Xunit;

namespace TodoListWebApi.Tests.Mappers
{
    //Structure of test method name is Method_Scenario_ExpectedResult
    public class TodoMapperTests

    {
        [Fact]
        public void Map_HasNullInput_ThrowsNullpointerException()
        {
            MongoTodoItem item = null;
            Assert.Throws<NullReferenceException>(() => TodoMapper.Map(item));
        }

        [Fact]
        public void Map_HasDataInMongoTodoItem_MapsToCorrectTodoItem()
        {
            var mongoItem = new MongoTodoItem()
            {
                Id = "MyAwesomeId1",
                Description = "Buy the all ellusive milk please",
                Done = false
            };

            var todo = TodoMapper.Map(mongoItem);
            Assert.NotNull(todo);
            Assert.Equal(mongoItem.Done, todo.Done);
            Assert.Equal(mongoItem.Id, todo.Id);
            Assert.Equal(mongoItem.Description, todo.Text);
        }

        [Fact]
        public void Map_HasValidTodoItem_MapsToCorrectMongoTodoItem()
        {
            var item = new TodoItem()
            {
                Id = "MySuperAwesomeId2",
                Text = "Pick up the kids in the kindergarten after work",
                Done = true
            };

            var mongoItem = TodoMapper.Map(item);
            Assert.NotNull(mongoItem);
            Assert.Equal(item.Done, mongoItem.Done);
            Assert.Equal(item.Id, mongoItem.Id);
            Assert.Equal(item.Text, mongoItem.Description);
        }
    }
}
