
using Moq;
using System.Collections.Generic;
using TodoListWebApi.Controllers;
using TodoListWebApi.Models;
using TodoListWebApi.Repo;
using Xunit;

namespace TodoListWebApi.Tests.Mappers
{
    public class TodoControllerTests : IClassFixture<TodoControllerTests>
    {
        private Mock<ITodoRepo> todoRepoMock;
        private IList<TodoItem> todos;
        //Setup method in xunit
        public TodoControllerTests()
        {
            todoRepoMock = new Mock<ITodoRepo>();
            todos = CreateTestData();

            todoRepoMock.Setup(instance => instance.FindById(todos[0].Id)).ReturnsAsync(todos[0]);
            todoRepoMock.Setup(instance => instance.FindById(todos[1].Id)).ReturnsAsync(todos[1]);
            todoRepoMock.Setup(instance => instance.FindById(todos[2].Id)).ReturnsAsync(todos[2]);
        }

        [Fact]
        public async void Get_WithId_CallsRepoFindById()
        {
          

            var controller = CreateController(todoRepoMock);
            var item = await controller.Get(todos[0].Id);

            todoRepoMock.Verify(repo => repo.FindById(It.IsAny<string>()), Times.AtMostOnce());

        }

        [Fact]
        public async void Get_NoParmas_ReturnsAllTodos()
        {
            todoRepoMock.Setup(instance => instance.GetTodos()).ReturnsAsync(todos);

            var controller = CreateController(todoRepoMock);

            var foundTodos = await controller.Get();

            todoRepoMock.Verify(repo => repo.GetTodos(), Times.AtMostOnce());
        }

        [Fact]
        public async void Delete_IdInParmas_CallsRepoDelete()
        {
            todoRepoMock.Setup(instance => instance.Delete(todos[0].Id)).Callback(() =>
            {
            });

            var controller = CreateController(todoRepoMock);

            await controller.Delete(todos[0].Id);

            todoRepoMock.Verify(repo => repo.Delete(todos[0].Id), Times.AtMostOnce());
        }


        private static TodoController CreateController(IMock<ITodoRepo> mockRepo)
        {
            var controller = new TodoController(mockRepo.Object)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new System.Web.Http.HttpConfiguration()
            };
            return controller;
        }

        private static IList<TodoItem> CreateTestData()
        {
            var todos = new List<TodoItem>() {
                new TodoItem()
                {
                    Id = "1",
                    Done = false,
                    Text = "First item"
                },
                new TodoItem()
                {
                    Id = "2",
                    Done = false,
                    Text = "Second item"
                },
                new TodoItem()
                {
                    Id = "3",
                    Done = false,
                    Text = "Third item"
                }
            };
            return todos;
        }
    }
}
