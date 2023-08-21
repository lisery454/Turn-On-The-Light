using System;
using System.Collections.Generic;

namespace Moss
{
    public partial class Container
    {
        public Dictionary<BindIdentifier, ISystem> SystemBindings { get; private set; }

        private void _InitSystemPart()
        {
            SystemBindings = new Dictionary<BindIdentifier, ISystem>();
        }

        public void SetSystem<TA, TB>(string id = null) where TB : TA where TA : ISystem
        {
            var bindIdentifier = BindIdentifier.New(typeof(TA), id);
            if (SystemBindings.ContainsKey(bindIdentifier))
                throw new Exception($"容器中已经存在{bindIdentifier}");
            var instance = Activator.CreateInstance<TB>();
            SystemBindings[bindIdentifier] = instance;
        }

        public T GetSystem<T>(string id = null) where T : ISystem
        {
            var bindIdentifier = BindIdentifier.New(typeof(T), id);
            var isExist = SystemBindings.TryGetValue(bindIdentifier, out var system);
            if (isExist) return (T)system;
            throw new Exception($"容器中找不到{bindIdentifier}");
        }

        #region System Cycles

        public void Update()
        {
            SystemBindings.Foreach((_, system) => { system.Update(); });
        }

        public void FixedUpdate()
        {
            SystemBindings.Foreach((_, system) => { system.FixedUpdate(); });
        }

        public void Awake()
        {
            SystemBindings.Foreach((_, system) => { system.Awake(); });
        }

        public void Start()
        {
            SystemBindings.Foreach((_, system) => { system.Start(); });
        }

        public void LateUpdate()
        {
            SystemBindings.Foreach((_, system) => { system.LateUpdate(); });
        }

        public void OnDestroy()
        {
            SystemBindings.Foreach((_, system) => { system.OnDestroy(); });
        }

        #endregion
    }
}