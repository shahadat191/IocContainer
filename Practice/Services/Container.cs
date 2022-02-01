using Practice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Services
{
    public class Container : IContainer
    {
        private readonly Dictionary<MappingKey, Func<object>> mappings;
        public Container()
        {
            mappings = new Dictionary<MappingKey, Func<object>>();
        }
        public bool IsRegistered(Type type, string instanceName = null)
        {
            var key = new MappingKey(type, instanceName);
            return mappings.ContainsKey(key);
        }

        public bool IsRegistered<T>(string instanceName = null)
        {
            return IsRegistered(typeof(T), instanceName);
        }

        public void Register(Type from, Func<object> createInstanceDelegate, string instanceName = null)
        {
            var key = new MappingKey(from, instanceName);
            if(mappings.ContainsKey(key))
            {
                throw new InvalidOperationException($"Ther requested mapping already exist.");
            }
            mappings.Add(key, createInstanceDelegate);
        }

        public void Register<T>(Func<T> createInstanceDelegate, string instanceName = null)
        {
            var createInstance = createInstanceDelegate as Func<object>;
            Register(typeof(T), createInstance, instanceName);
        }

        private object Create(Type type)
        {
            //Find a default constructor using reflection
            var defaultConstructor = type.GetConstructors()[0];
            //Verify if the default constructor requires params
            var defaultParams = defaultConstructor.GetParameters();
            //Instantiate all constructor parameters using recursion
            var parameters = defaultParams.Select(param => Create(param.ParameterType)).ToArray();
            return defaultConstructor.Invoke(parameters);
        }


        public void Register(Type from, Type to, string instanceName = null)
        {
            if (to == null)
                throw new ArgumentNullException("to");
            if(from.IsAssignableFrom(to) == false)
            {
                throw new InvalidOperationException($"Error trying to register from {from.FullName} to {to.FullName}");
            }
            Func<object> createInstanceDelegate = () => Create(to);
            //Func<object> createInstanceDelegate = () => Activator.CreateInstance(to);
            Register(from, createInstanceDelegate, instanceName);
        }
        public void Register<TFrom, TTo>(string instaceName = null) where TTo : TFrom
        {
            Register(typeof(TFrom), typeof(TTo), instaceName);
        }



        public object Resolve(Type type, string instanceName = null)
        {
            var key = new MappingKey(type, instanceName);
            Func<object> createInstance;
            if(mappings.TryGetValue(key, out createInstance))
            {
                return createInstance();
            }
            throw new InvalidOperationException($"Cannot resolve {type.FullName}");

        }

        public T Resolve<T>(string instanceName = null)
        {
            return (T)Resolve(typeof(T), instanceName);
        }
    }
}
