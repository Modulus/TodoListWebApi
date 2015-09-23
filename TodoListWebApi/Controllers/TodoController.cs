using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TodoListWebApi.Controllers
{
    public class TodoController : ApiController
    {
        // GET: api/Todo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Todo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Todo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Todo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Todo/5
        public void Delete(int id)
        {
        }
    }
}
