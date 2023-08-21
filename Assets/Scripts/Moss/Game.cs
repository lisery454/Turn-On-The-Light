using UnityEngine;

namespace Moss
{
    public class Game : PostInitSingleton<Game>, IPostInitSingleton
    {
        /// <summary>
        ///  上下文信息，包括需要注入的对象
        /// </summary>
        public IContext Context { get; set; }

        public CommandExecutor CommandExecutor { get; private set; }

        public EventDispatcher EventDispatcher { get; private set; }

        public FrameworkConfig FrameworkConfig { get; private set; }

        public void Init()
        {
            FrameworkConfig = FrameworkConfig.Load();
            EventDispatcher = new EventDispatcher();
            CommandExecutor = new CommandExecutor();
            Context = null;
        }

        [RuntimeInitializeOnLoadMethod]
        private static void ForceInitGame()
        {
            var _ = Instance;
        }
    }
}