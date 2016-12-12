using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using Sample.WebApi.Configuration.Installers;
using Sample.WebApi.Configuration.StartupTasks;
using Swashbuckle.Application;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Sample.WebApi.Configuration
{
    public class WebStartUp
    {
        public void Configuration(IAppBuilder app)
        {
            var containerBuilder = new ContainerBuilder();
            var executingAssembly = Assembly.GetExecutingAssembly();

            containerBuilder.RegisterInstance(GlobalConfiguration.Configuration);            

            var config = GlobalConfiguration.Configuration;
            config.EnableSwagger(c => c.SingleApiVersion("v1", "Sample API")).EnableSwaggerUi();

            containerBuilder.RegisterStartupTasks(executingAssembly);
            containerBuilder.RegisterApiControllers(executingAssembly);
            containerBuilder.RegisterRepositories(executingAssembly);

            var container = containerBuilder.Build();

            using (var lifeTimeScope = container.BeginLifetimeScope())
            {
                var r = lifeTimeScope.Resolve<IEnumerable<IStartupTask>>();
                r.ToList().ForEach(x => x.Run());
            }

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}