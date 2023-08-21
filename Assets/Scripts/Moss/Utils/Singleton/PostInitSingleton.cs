namespace Moss
{
    public abstract class PostInitSingleton<T> where T : IPostInitSingleton, new()
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = new T();

                instance.Init();

                return instance;
            }
        }
    }
}