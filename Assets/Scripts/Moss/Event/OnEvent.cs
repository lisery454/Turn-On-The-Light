namespace Moss
{
    public delegate void OnEvent<in TEvent>(TEvent e, object source) where TEvent : IEvent;
}