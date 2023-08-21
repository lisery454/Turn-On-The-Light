using UnityEngine;

namespace TOTL
{
    public class Lamp : MonoBehaviour
    {
        private bool _isLit;

        [SerializeField] private GameObject mask;

        [SerializeField] private Sprite litSprite;
        [SerializeField] private Sprite unlitSprite;

        [SerializeField] private SpriteRenderer sr;

        public bool IsLit
        {
            get => _isLit;
            set
            {
                _isLit = value;
                mask.SetActive(_isLit);
                sr.sprite = _isLit ? litSprite : unlitSprite;
            }
        }

        private void Awake()
        {
            IsLit = mask.activeSelf;
        }


        public bool IsInLight(Vector2 pos)
        {
            if (_isLit)
            {
                var lightPos = transform.position;

                return Mathf.Abs(lightPos.x - pos.x) <= 2 && Mathf.Abs(lightPos.y - pos.y) <= 2;
            }

            return false;
        }
    }
}