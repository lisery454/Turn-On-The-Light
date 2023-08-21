using System;
using System.Collections.Generic;

namespace Moss
{
    public partial class Container
    {
        public Dictionary<BindIdentifier, IState> StateBindings { get; private set; }

        private void _InitStatePart()
        {
            StateBindings = new Dictionary<BindIdentifier, IState>();
        }

        public T GetState<T>(string id = null) where T : IState
        {
            var bindIdentifier = BindIdentifier.New(typeof(T), id);
            var isExist = StateBindings.TryGetValue(bindIdentifier, out var state);
            if (isExist) return (T)state;
            throw new Exception($"容器中找不到{bindIdentifier}");
        }

        public void SetState<TA, TB>(string id = null) where TA : IState where TB : TA
        {
            var bindIdentifier = BindIdentifier.New(typeof(TA), id);
            if (StateBindings.ContainsKey(bindIdentifier))
                throw new Exception($"容器中已经存在{bindIdentifier}");
            var instance = Activator.CreateInstance<TB>();
            StateBindings[bindIdentifier] = instance;
        }
    }
}