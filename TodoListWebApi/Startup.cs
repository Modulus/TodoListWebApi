﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TodoListWebApi.Startup))]

namespace TodoListWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
