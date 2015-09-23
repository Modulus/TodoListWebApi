using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoListWebApi.Models
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Done { get; set; }
    }
}