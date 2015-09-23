using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListWebApi.Models;

namespace TodoListWebApi.Repo
{
    public interface ITodoRepo

    {
        Task<IList<TodoItem>> GetTodos();
        Task<TodoItem> MarkAsDone(string todoId);
        Task<bool> Exists(string todoId);
        Task<TodoItem> MaskAsNotDone(string todoId);
        Task<TodoItem> FindById(string todoId);
        Task Delete(string todoId);
    }
}
