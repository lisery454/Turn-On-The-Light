namespace Moss
{
    public abstract class Singleton<T> where T : new()
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = new T();
                return instance;
            }
        }
    }
}