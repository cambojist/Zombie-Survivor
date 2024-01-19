using UnityEngine;

namespace Assets.Scripts
{
    public class Reposition : MonoBehaviour
    {
        private Collider2D _collider;

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Area"))
            {
                return;
            }

            var playerPos = GameManager.instance.player.transform.position;
            var groundPos = transform.position;
            var diffX = Mathf.Abs(playerPos.x - groundPos.x);
            var diffY = Mathf.Abs(playerPos.y - groundPos.y);

            var playerDir = GameManager.instance.player.InputDirection;
            var dirX = playerDir.x < 0 ? -1 : 1;
            var dirY = playerDir.y < 0 ? -1 : 1;

            switch (transform.tag)
            {
                case "Ground":
                    if (diffX > diffY)
                    {
                        transform.Translate(40 * dirX * Vector2.right);
                    }
                    else if (diffX < diffY)
                    {
                        transform.Translate(40 * dirY * Vector2.up);
                    }
                    break;
                case "Enemy":
                    if (_collider.enabled)
                    {
                        transform.Translate(playerDir * 20 + new Vector2(Random.Range(-3, 3), Random.Range(-3, 3)));
                    }
                    break;
            }

        }
    }
}