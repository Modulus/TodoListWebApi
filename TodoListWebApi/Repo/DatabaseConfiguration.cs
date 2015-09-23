using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoListWebApi.Repo
{
    public class DatabaseConfiguration
    {
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}