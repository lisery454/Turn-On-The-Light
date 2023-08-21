using System.Collections.Generic;

namespace Moss
{
    public class EventDispatcher
    {
        private readonly Dictionary<string, IEventScope> _eventScopes = new();

        private void _AddEventScopeIfNotExist(string scopeName)
        {
            _eventScopes.TryAdd(scopeName, new EventScope());
        }


        public void Register<TEvent>(object registerer, OnEvent<TEvent> onEvent, string scopeName)
            where TEvent : IEvent
        {
            _AddEventScopeIfNotExist(scopeName);
            _eventScopes[scopeName].Register(registerer, onEvent);
        }

        public void Unregister<TEvent>(object unregisterer, OnEvent<TEvent> onEvent, string scopeName)
            where TEvent : IEvent
        {
            _AddEventScopeIfNotExist(scopeName);
            _eventScopes[scopeName].Unregister(unregisterer, onEvent);
        }

        public void TriggerToAll<TEvent>(object source, TEvent e, string scopeName) where TEvent : IEvent
        {
            _AddEventScopeIfNotExist(scopeName);
            _eventScopes[scopeName].TriggerToAll(source, e);
        }

        public void TriggerToObj<TEvent>(object source, TEvent e, object obj, string scopeName)
            where TEvent : IEvent
        {
            _AddEventScopeIfNotExist(scopeName);
            _eventScopes[scopeName].TriggerToObj(source, e, obj);
        }
    }
}