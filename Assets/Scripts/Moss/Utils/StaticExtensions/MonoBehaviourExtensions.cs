using UnityEngine;

namespace Moss
{
    public static class MonoBehaviourExtensions
    {
        #region Event

        public static void Register<TEvent>(this MonoBehaviour monoBehaviour, OnEvent<TEvent> onEvent,
            string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.Register(monoBehaviour, onEvent, scopeName);
        }

        public static void Unregister<TEvent>(this MonoBehaviour monoBehaviour, OnEvent<TEvent> onEvent,
            string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.Unregister(monoBehaviour, onEvent, scopeName);
        }

        public static void TriggerToAll<TEvent>(this MonoBehaviour monoBehaviour, TEvent e, string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.TriggerToAll(monoBehaviour, e, scopeName);
        }

        public static void TriggerToObj<TEvent>(this MonoBehaviour monoBehaviour, TEvent e, object obj,
            string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.TriggerToObj(monoBehaviour, e, obj, scopeName);
        }

        #endregion
    }
}