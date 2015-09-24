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
        Task<bool> Exists(string id);
        Task<bool> ExistsByText(string text);
        Task<TodoItem> MaskAsNotDone(string todoId);
        Task<TodoItem> FindById(string todoId);
        Task Delete(string todoId);
        Task<TodoItem> Save(TodoItem mongoItem);
    }
}
