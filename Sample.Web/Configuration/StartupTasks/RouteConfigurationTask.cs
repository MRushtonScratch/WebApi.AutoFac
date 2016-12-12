using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Sample.WebApi.Configuration.StartupTasks
{
    public class RouteConfigurationTask : IStartupTask
    {
        private readonly HttpConfiguration _configuration;

        public RouteConfigurationTask(HttpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Run()
        {
            _configuration.MapHttpAttributeRoutes();

            _configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            _configuration.EnsureInitialized();
        }
    }
}