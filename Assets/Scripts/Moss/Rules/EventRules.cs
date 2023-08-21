using UnityEngine;

namespace Moss
{
    public static class EventRules
    {
        public static void Register<TEvent>(this MonoBehaviour registerer, OnEvent<TEvent> onEvent,
            string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.Register(registerer, onEvent, scopeName);
        }

        public static void Unregister<TEvent>(this MonoBehaviour unregisterer, OnEvent<TEvent> onEvent,
            string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.Unregister(unregisterer, onEvent, scopeName);
        }

        public static void Register<TEvent>(this ISystem registerer, OnEvent<TEvent> onEvent,
            string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.Register(registerer, onEvent, scopeName);
        }

        public static void Unregister<TEvent>(this ISystem unregisterer, OnEvent<TEvent> onEvent,
            string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.Unregister(unregisterer, onEvent, scopeName);
        }


        public static void TriggerToAll<TEvent>(this IService source, TEvent e, string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.TriggerToAll(source, e, scopeName);
        }

        public static void TriggerToObj<TEvent>(this IService source, TEvent e, object obj, string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.TriggerToObj(source, e, obj, scopeName);
        }
    }
}