using UnityEngine;

namespace Moss
{
    public static class ColorExtensions
    {
        public static string ToHexStr(this Color self)
        {
            return ColorUtility.ToHtmlStringRGB(self);
        }
    }
}