using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string name)
        {
            Console.WriteLine(name);
        }
    }
    public class UselessLogger : ILogger
    {
        public void Write(string name)
        {
            
        }
    }

    public interface ILogger
    {
        public void Write(string name);
        
    }
}
