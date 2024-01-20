using UnityEngine;

namespace Assets.Scripts
{
    public class Follow : MonoBehaviour
    {
        private RectTransform _rect;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void FixedUpdate()
        {
            _rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
        }
    }
}