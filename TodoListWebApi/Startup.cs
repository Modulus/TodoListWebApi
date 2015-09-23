using System.Web.Http;
using Autofac;

using Microsoft.Owin;
using Microsoft.Owin.Cors;
using MongoDB.Driver;
using Owin;
using TodoListWebApi.Repo;
using Autofac.Integration.WebApi;
using System.Reflection;

[assembly: OwinStartup(typeof(ComWeb.Services.RestApi.Startup))]

namespace ComWeb.Services.RestApi
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration;

            var mongoConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MongoConnectionString"].ConnectionString;
            var mongoDatabaseName = System.Configuration.ConfigurationManager.AppSettings["MongoDatabaseName"];
            var mongoTodoCollectionName = System.Configuration.ConfigurationManager.AppSettings["MongoTodoCollectionName"];

       
            app.UseAutofacWebApi(config);

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

            builder.RegisterWebApiFilterProvider(config);

            // Register Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();

            ConfigureAuth(app);

            var container = builder.Build();

            // Create an assign a dependency resolver for Web API to use.
            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;

            app.UseAutofacMiddleware(container);
        }

     
    }
}
