using Gtmooaf.Interfaces;

namespace Gtmooaf.Services
{
    public class GreeterService : IGreeterService
    {
        public string Greet(string name) => $"Hi there, {name}!";
    }
}
