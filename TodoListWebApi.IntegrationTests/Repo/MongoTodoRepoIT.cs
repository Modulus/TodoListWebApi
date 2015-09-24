using MongoDB.Driver;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListWebApi.Repo;
using Xunit;
using TodoListWebApi.Models;

namespace TodoListWebApi.IntegrationTests
{
    public class MongoTodoRepoIT :  IClassFixture<MongoTodoRepoIT>
    {
        private static IMongoClient _mongoClient;
        private static ITodoRepo _repo;
        private static DatabaseConfiguration config = new DatabaseConfiguration() { CollectionName = "todos", DatabaseName = "todoBase" };
        public MongoTodoRepoIT()
        {
            var mongoConnectionString = ConfigurationManager.ConnectionStrings["MongoConnectionString"].ConnectionString;
            _mongoClient = new MongoClient(mongoConnectionString);
            _repo = new MongoTodoRepo(_mongoClient, config);
        }

        [Fact]
        public async void Save_TodoItemHasValidDataButNoId_SavedToMongoDB()
        {
            var item = new TodoItem() { Done = false, Text = "Go to job interview" , Id=null};

            Assert.Null(item.Id);

            var savedItem = await _repo.Save(item);


            Assert.NotNull(savedItem.Id);
            var exists = await _repo.ExistsByText(item.Text);
            Assert.True(exists);
            await _repo.Delete(savedItem.Id);
        }

        [Fact]
        public async void Save_TodoItemExistsButModified_GetsUpdates()
        {
            var item = new TodoItem() { Done = false, Text = "Go to job interview" };

            Assert.Null(item.Id);

            var savedItem = await _repo.Save(item);

            savedItem.Text = "New updated text";
            item = savedItem;

            savedItem = await _repo.Save(savedItem);

            Assert.Equal(item.Id, savedItem.Id);
            await _repo.Delete(savedItem.Id);
        }

        [Fact]
        public async void Delete_ItemExists_RemovedNoLongerExists()
        {
            var item = new TodoItem() { Done = false, Text = "Go to job interview", Id = null };

            Assert.Null(item.Id);

            var savedItem = await _repo.Save(item);

            await _repo.Delete(savedItem.Id);

            var exists = await _repo.ExistsByText(savedItem.Text);
            Assert.False(exists);

        }
    }
}
