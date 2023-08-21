using System;

namespace Moss
{
    public class ObservableProperty<T> where T : IEquatable<T>
    {
        private T _value;


        public T Value
        {
            get => _value;
            set
            {
                var originalValue = _value;
                _value = value;
                if (!originalValue.Equals(value))
                {
                    OnValueChanged?.Invoke(value);
                }
            }
        }

        private event Action<T> OnValueChanged;

        public ObservableProperty(T value)
        {
            _value = value;
        }

        public void ClearOnValueChanged()
        {
            OnValueChanged = null;
        }

        public void RegisterOnValueChanged(Action<T> action)
        {
            OnValueChanged += action;
        }
        
        public void UnRegisterOnValueChanged(Action<T> action)
        {
            OnValueChanged -= action;
        }

        public void RegisterOnValueChangedAndInit(Action<T> action)
        {
            RegisterOnValueChanged(action);
            OnValueChanged?.Invoke(_value);
        }
    }
}