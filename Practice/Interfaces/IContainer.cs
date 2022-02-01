using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Interfaces
{
    public interface IContainer
    {
        void Register(Type from, Type to, string instanceName = null);
        void Register<TFrom, TTo>(string instaceName = null) where TTo : TFrom;

        void Register(Type from, Func<object> createInstanceDelegate, string instanceName = null);
        void Register<T>(Func<T> createInstanceDelegate, string instanceName = null);
        bool IsRegistered(Type type, string instanceName = null);
        bool IsRegistered<T>(string instanceName = null);
        object Resolve(Type type, string instanceName = null);
        T Resolve<T>(string instanceName = null);





    }
}
