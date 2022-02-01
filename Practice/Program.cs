using Practice.Services;
using System;

namespace Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.Register<ILogger, ConsoleLogger>();

            ILogger logger = container.Resolve<ILogger>();
            logger.Write("Foo");
        }

        
    }
}
