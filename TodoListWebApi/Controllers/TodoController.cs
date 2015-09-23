using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TodoListWebApi.Models;
using TodoListWebApi.Repo;

namespace TodoListWebApi.Controllers
{
    public class TodoController : ApiController
    {
        private ITodoRepo _repo;
        public TodoController(ITodoRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Todo
        [AllowAnonymous]
        public async Task<IEnumerable<TodoItem>> Get()
        {
            return await _repo.GetTodos();
        }

        // GET: api/Todo/5
        public async Task<TodoItem> Get(string id)
        {
            return await _repo.FindById(id);
        }

        // POST: api/Todo
        public async Task<HttpResponseMessage> Post(TodoItem todoItem)
        {
            if( todoItem == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No data was posted");
            }

            return await SaveTodoAndGetResponse(todoItem);
        }

        // PUT: api/Todo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Todo/5
        public async Task Delete(string id)
        {
           await _repo.Delete(id);
        }

        private async Task<HttpResponseMessage> SaveTodoAndGetResponse(TodoItem item)
        {
            var exists = await _repo.Exists(item);
            if (item.Id == null && exists == false)
            {

                var mongoItem = new MongoTodoItem()
                {
                    Done = item.Done,
                    Description = item.Text
                };


                await _repo.Save(item);

                return Request.CreateResponse<string>(HttpStatusCode.Created, "Todo created");
            }
            else
            {
                return Request.CreateResponse<string>(HttpStatusCode.Conflict, "Item allready exists in database");
            }

        }
    }
}
