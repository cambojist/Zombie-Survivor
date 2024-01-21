using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public Vector2 InputDirection { get; private set; }
        public Scanner scanner;
        public Hand[] hands;
        public float speed = 3;

        private Rigidbody2D _rigidBody;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            scanner = GetComponent<Scanner>();
            hands = GetComponentsInChildren<Hand>(true);
        }

        private void FixedUpdate()
        {
            if (!GameManager.instance.isAlive)
            {
                return;
            }
            var move = InputDirection * Time.fixedDeltaTime * speed;
            _rigidBody.MovePosition(_rigidBody.position + move);
        }

        private void LateUpdate()
        {
            if (!GameManager.instance.isAlive)
            {
                return;
            }
            _animator.SetFloat("Speed", InputDirection.magnitude);
            if (InputDirection.x != 0)
            {
                _spriteRenderer.flipX = InputDirection.x < 0;
            }
        }

        private void OnMove(InputValue value)
        {
            if (!GameManager.instance.isAlive)
            {
                return;
            }
            InputDirection = value.Get<Vector2>();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!GameManager.instance.isAlive)
            {
                return;
            }

            GameManager.instance.health -= Time.deltaTime * 10;

            if (GameManager.instance.health < 0)
            {
                for (int i = 2; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }

                _animator.SetTrigger("Dead");
                GameManager.instance.GameOver();
            }
        }
    }
}