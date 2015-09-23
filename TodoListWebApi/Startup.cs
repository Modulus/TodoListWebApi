using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Autofac;
using TodoListWebApi.Repo;
using MongoDB.Driver;

[assembly: OwinStartup(typeof(TodoListWebApi.Startup))]

namespace TodoListWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration;

            var mongoConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MongoConnectionString"].ConnectionString;
            var mongoDatabaseName = System.Configuration.ConfigurationManager.AppSettings["MongoDatabaseName"];
            var mongoTodoCollectionName = System.Configuration.ConfigurationManager.AppSettings["MongoTodoCollectionName"];


            //For rest call on another domain
            //NB! Remove this before production
            app.UseCors(CorsOptions.AllowAll);

            var builder = new ContainerBuilder();

            //MongoClient singleton preferred, Thread safe with multiple connecitons. 
            //The mongoClient is a conneciton pool, hence only one is needed.
            //More info: http://blog.mongodb.org/post/94065240033/getting-started-with-mongodb-and-java-part-i
            var mongoClient = new MongoClient(mongoConnectionString);
            builder.RegisterInstance(mongoClient).As<IMongoClient>().SingleInstance();

            var databaseConfig = new DatabaseConfiguration()
            {
                DatabaseName = mongoDatabaseName,
                CollectionName = mongoTodoCollectionName
            };
            builder.RegisterInstance(databaseConfig);

            builder.RegisterType<MongoTodoRepo>().UsingConstructor(typeof(IMongoClient), typeof(DatabaseConfiguration)).As<ITodoRepo>();






        }
    }
}
