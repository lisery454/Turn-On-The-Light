using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Moss
{
    public interface IEventScope
    {
        void Register<TEvent>(object registerer, OnEvent<TEvent> onEvent) where TEvent : IEvent;
        void Unregister<TEvent>(object unregisterer, OnEvent<TEvent> onEvent) where TEvent : IEvent;

        void TriggerToAll<TEvent>(object source, TEvent e) where TEvent : IEvent;
        void TriggerToObj<TEvent>(object source, TEvent e, object obj) where TEvent : IEvent;
    }

    public class EventScope : IEventScope
    {
        // 事件类型 -> EventListeners
        private Dictionary<Type, List<object>> EventListeners { get; } = new();

        // 暂时性容器，保存listener，等下一次Trigger时加入eventListeners
        private readonly Dictionary<Type, List<object>> _listenersRegister = new();
        private readonly Dictionary<Type, List<object>> _listenersUnregister = new();

        public void Register<TEvent>(object registerer, OnEvent<TEvent> onEvent) where TEvent : IEvent
        {
            var type = typeof(TEvent);
            var eventListener = new EventListener<TEvent>(registerer, onEvent);
            _listenersRegister.AddElementToListValue(type, eventListener);
        }

        public void Unregister<TEvent>(object unregisterer, OnEvent<TEvent> onEvent) where TEvent : IEvent
        {
            var type = typeof(TEvent);
            var eventListener = new EventListener<TEvent>(unregisterer, onEvent);
            _listenersUnregister.AddElementToListValue(type, eventListener);
        }

        public void TriggerToAll<TEvent>(object source, TEvent e) where TEvent : IEvent
        {
            _AddNewOnEvent();
            _RemoveOldOnEvent();

            if (!EventListeners.TryGetValue(e.GetType(), out var listeners)) return;
            
            // 清除所有侦听器注册者为null的侦听器
            listeners.RemoveAll(listener => ((EventListener<TEvent>)listener).registerer == null);

            //寻找所有该事件的侦听器，并且侦听器的注册者要不为null
            listeners.Cast<EventListener<TEvent>>().Foreach(listener => { listener.onEvent.Invoke(e, source); });
        }

        public void TriggerToObj<TEvent>(object source, TEvent e, object obj) where TEvent : IEvent
        {
            _AddNewOnEvent();
            _RemoveOldOnEvent();

            if (!EventListeners.TryGetValue(e.GetType(), out var listeners)) return;
            
            // 清除所有侦听器注册者为null的侦听器
            listeners.RemoveAll(listener => ((EventListener<TEvent>)listener).registerer == null);

            // 寻找所有该事件的侦听器，并且侦听器的注册者要不为null
            listeners.Cast<EventListener<TEvent>>()
                .Where(listener => listener.registerer == obj)
                .Foreach(listener => { listener.onEvent.Invoke(e, source); });
        }

        private void _AddNewOnEvent()
        {
            // 添加新注册的listener
            _listenersRegister.Foreach((type, listeners) => { EventListeners.AddElementsToListValue(type, listeners); });

            _listenersRegister.Clear();
        }

        private void _RemoveOldOnEvent()
        {
            // 移除要移除的listener
            _listenersUnregister.Foreach((type, listeners) =>
            {
                if (EventListeners.TryGetValue(type, out var listener))
                    listener.RemoveElements(listeners); 
            });

            _listenersUnregister.Clear();
        }
    }
}