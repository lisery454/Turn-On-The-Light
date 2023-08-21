using System;
using UnityEngine;

namespace Moss
{
    public static class TransformExtensions
    {
        public static void ForeachChild(this Transform self, Action<Transform> action)
        {
            for (var i = 0; i < self.childCount; i++)
            {
                action?.Invoke(self.GetChild(i));
            }
        }

        public static void SetRotationZ(this Transform self, float z)
        {
            var selfEulerAngles = self.localRotation.eulerAngles;
            selfEulerAngles.z = z;
            self.localRotation = Quaternion.Euler(selfEulerAngles);
        }
    }
}