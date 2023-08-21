using System;
using System.Collections.Generic;

namespace Moss
{
    public partial class Container
    {
        public Dictionary<BindIdentifier, IService> ServiceBindings { get; private set; }

        private void _InitServicePart()
        {
            ServiceBindings = new Dictionary<BindIdentifier, IService>();
        }

        public T GetService<T>(string id = null) where T : IService
        {
            var bindIdentifier = BindIdentifier.New(typeof(T), id);
            var isExist = ServiceBindings.TryGetValue(bindIdentifier, out var service);
            if (isExist) return (T)service;
            throw new Exception($"容器中找不到{bindIdentifier}");
        }

        public void SetService<TA, TB>(string id = null) where TA : IService where TB : TA
        {
            var bindIdentifier = BindIdentifier.New(typeof(TA), id);
            if (ServiceBindings.ContainsKey(bindIdentifier))
                throw new Exception($"容器中已经存在{bindIdentifier}");
            var instance = Activator.CreateInstance<TB>();
            injector.Inject(injector, InjectFlag.State);
            ServiceBindings[bindIdentifier] = instance;
        }
    }
}