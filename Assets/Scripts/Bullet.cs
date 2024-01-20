using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : MonoBehaviour
    {
        public float damage;
        public int per;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Init(float damage, int per, Vector2 dir)
        {
            this.damage = damage;
            this.per = per;

            if (per > -1)
            {
                _rigidbody.velocity = dir * 15;
            }
        }

        public void Init(float damage, int per)
        {
            Init(damage, per, Vector2.zero);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Enemy") || per == -1)
            {
                return;
            }
            per--;

            if (per == -1)
            {
                _rigidbody.velocity = Vector2.zero;
                gameObject.SetActive(false);
            }

        }
    }
}