using Sample.WebApi.Application;

namespace Sample.WebApi.Infrastructure
{
    public class SampleRepository : IRepository
    {
        public bool Initialised ()
        {
            return true;
        }
    }
}