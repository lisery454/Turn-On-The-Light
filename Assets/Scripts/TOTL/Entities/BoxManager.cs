using System.Collections.Generic;
using Moss;
using UnityEngine;

namespace TOTL
{
    public class BoxManager : MonoSingleton<BoxManager>
    {
        protected override void Awake()
        {
            base.Awake();
            UpdateBox();
        }

        public bool IsInLight(Vector2 pos)
        {
            var isInLight = false;
            transform.ForeachChild(trans =>
            {
                if (trans.gameObject.TryGetComponent<Lamp>(out var lamp))
                {
                    if (lamp.IsInLight(pos)) isInLight = true;
                }
            });

            return isInLight;
        }

        public bool TryGetBox(Vector2 pos, out GameObject obj)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (!(Vector3.Distance(child.position, pos) <= 0.1f)) continue;
                obj = child.gameObject;
                return true;
            }

            obj = null;
            return false;
        }

        public void UpdateBox()
        {
            UpdateElectricity();
            UpdateLampNum();
        }

        public bool IsLock(GameObject obj)
        {
            return obj.TryGetComponent<Lock>(out _);
        }

        private void UpdateElectricity()
        {
            var dirs = new List<Vector3> { Vector3.left, Vector3.right, Vector3.up, Vector3.down };
            transform.ForeachChild(trans =>
            {
                if (trans.gameObject.TryGetComponent<Lamp>(out var lamp))
                {
                    var hasElectricityAround = false;
                    foreach (var dir in dirs)
                    {
                        var isBox = TryGetBox(lamp.transform.position + dir, out var obj);
                        if (!isBox) continue;
                        var electricity = obj.GetComponent<Electricity>();
                        if (electricity != null)
                        {
                            hasElectricityAround = true;
                            break;
                        }
                    }

                    lamp.IsLit = hasElectricityAround;
                }
            });
        }

        private void UpdateLampNum()
        {
            var allCount = 0;
            var litCount = 0;
            transform.ForeachChild(trans =>
            {
                if (trans.gameObject.TryGetComponent<Lamp>(out var lamp))
                {
                    allCount++;
                    if (lamp.IsLit) litCount++;
                }
            });
            UIManager.Instance.UpdateLampNum(litCount, allCount);
        }

        public bool IsAllLight()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<Lamp>(out var lamp))
                {
                    if (!lamp.IsLit) return false;
                }
            }

            return true;
        }
    }
}