using UnityEngine;

namespace Moss
{
    public static class SpriteRenderExtensions
    {
        public static void SetAlpha(this SpriteRenderer self, float alpha)
        {
            var selfColor = self.color;
            selfColor.a = alpha;
            self.color = selfColor;
        }
    }
}