using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public Vector2 InputDirection { get; private set; }

        private float _speed = 3;

        private Rigidbody2D _rigidBody;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
        }

        private void FixedUpdate()
        {
            var move = InputDirection * Time.fixedDeltaTime * _speed;
            _rigidBody.MovePosition(_rigidBody.position + move);
        }

        private void LateUpdate()
        {
            _animator.SetFloat("Speed", InputDirection.magnitude);
            if (InputDirection.x != 0)
            {
                _spriteRenderer.flipX = InputDirection.x < 0;
            }
        }

        private void OnMove(InputValue value)
        {
            InputDirection = value.Get<Vector2>();
        }
    }
}