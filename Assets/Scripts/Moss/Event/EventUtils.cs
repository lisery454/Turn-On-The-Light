namespace Moss
{
    public static class EventUtils
    {
        public static void Register<TEvent>(object registerer, OnEvent<TEvent> onEvent, string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.Register(registerer, onEvent, scopeName);
        }

        public static void Unregister<TEvent>(object unregisterer, OnEvent<TEvent> onEvent, string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.Unregister(unregisterer, onEvent, scopeName);
        }

        public static void TriggerToAll<TEvent>(object source, TEvent e, string scopeName = null) where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.TriggerToAll(source, e, scopeName);
        }

        public static void TriggerToObj<TEvent>(object source, TEvent e, object obj, string scopeName = null)
            where TEvent : IEvent
        {
            Game.Instance.EventDispatcher.TriggerToObj(source, e, obj, scopeName);
        }
    }
}