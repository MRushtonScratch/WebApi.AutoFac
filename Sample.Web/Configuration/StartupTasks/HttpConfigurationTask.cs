using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sample.WebApi.Models;
using Swashbuckle.Application;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Sample.WebApi.Configuration.StartupTasks
{
    public class HttpConfigurationTask : IStartupTask
    {
        private readonly HttpConfiguration _configuration;
        public HttpConfigurationTask(HttpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Run()
        {
            _configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            _configuration.Formatters.JsonFormatter.SupportedMediaTypes.Clear();
            _configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(MediaTypes.SampleVersion1));

            //url append: swagger/ui/index
            //_configuration.EnableSwagger(c => c.SingleApiVersion("v1", "Sample API")).EnableSwaggerUi();
            
            _configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            _configuration.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            _configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            _configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}