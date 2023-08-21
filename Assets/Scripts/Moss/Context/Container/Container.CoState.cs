using System;
using System.Collections.Generic;

namespace Moss
{
    public partial class Container : IDestroyCoState
    {
        public Dictionary<BindIdentifier, Type> CoStateBindings { get; private set; }

        public Dictionary<BindIdentifier, Dictionary<object, ICoState>> CoStateBindingsRecord { get; private set; }

        private void _InitCoStatePart()
        {
            CoStateBindings = new Dictionary<BindIdentifier, Type>();
            CoStateBindingsRecord = new Dictionary<BindIdentifier, Dictionary<object, ICoState>>();
        }

        public void SetCoState<TA, TB>(string id = null) where TA : ICoState where TB : TA
        {
            var bindIdentifier = BindIdentifier.New(typeof(TA), id);
            if (CoStateBindings.ContainsKey(bindIdentifier))
                throw new Exception($"容器中已经存在{bindIdentifier}");
            CoStateBindings[bindIdentifier] = typeof(TB);
            CoStateBindingsRecord[bindIdentifier] = new Dictionary<object, ICoState>();
        }

        public T GetCoState<T>(object obj, string id = null) where T : ICoState
        {
            var bindIdentifier = BindIdentifier.New(typeof(T), id);
            // 是否存在对应的type
            var isExist = CoStateBindings.TryGetValue(bindIdentifier, out _);

            // 不存在报错
            if (!isExist) throw new Exception($"容器中找不到{bindIdentifier}");

            // 是否之前创建过实例了
            if (CoStateBindingsRecord[bindIdentifier].ContainsKey(obj))
                return (T)CoStateBindingsRecord[bindIdentifier][obj];

            // 没有创建过实例
            var instance = (T)Activator.CreateInstance(CoStateBindings[bindIdentifier]);
            CoStateBindingsRecord[bindIdentifier][obj] = instance;
            return instance;
        }

        public void DestroyCoState<T>(object obj, string id = null) where T : ICoState
        {
            var bindIdentifier = BindIdentifier.New(typeof(T), id);
            if (!CoStateBindings.ContainsKey(bindIdentifier)) return;
            if (!CoStateBindingsRecord[bindIdentifier].ContainsKey(obj)) return;
            CoStateBindingsRecord[bindIdentifier].Remove(obj);
        }
    }
}