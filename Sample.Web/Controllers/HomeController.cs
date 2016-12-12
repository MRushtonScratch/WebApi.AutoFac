using Sample.WebApi.Application;
using System.Web.Http;

namespace Sample.WebApi.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private IRepository _repository;
        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("initialised")]
        public bool Initialised()
        {
            return _repository.Initialised();
        } 
    }
}