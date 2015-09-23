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
    }
}
