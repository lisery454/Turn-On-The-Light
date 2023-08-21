using UnityEngine;

namespace Moss
{
    [CreateAssetMenu(menuName = "moss_config")]
    public class FrameworkConfig : ScriptableObject
    {
        public bool isLogEvent;
        public bool isLogContainerRegister;

        public static FrameworkConfig Load()
        {
            var config = Resources.Load<FrameworkConfig>($"moss_config");
            if (config != null) return config;

            var frameworkConfig = CreateInstance<FrameworkConfig>();
            frameworkConfig.isLogEvent = false;
            frameworkConfig.isLogContainerRegister = false;
            return frameworkConfig;
        }
    }
}