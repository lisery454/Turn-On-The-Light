namespace Moss
{
    public readonly struct EventListener<TEvent> where TEvent : IEvent
    {
        public readonly object registerer;
        public readonly OnEvent<TEvent> onEvent;

        public EventListener(object registerer, OnEvent<TEvent> onEvent)
        {
            this.registerer = registerer;
            this.onEvent = onEvent;
        }
    }
}