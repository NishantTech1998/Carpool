using System;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;

namespace CarPoolApp.Helper
{
    public class DependencyResolver
    {
        public static Container Container = new Container();

        public static T Get<T>() where T : class
        {
            if (Container == null) throw new InvalidOperationException("Cannot resolve dependencies before the container has been initialized.");
            return Container.GetInstance<T>();
        }
    }
}
