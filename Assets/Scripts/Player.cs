using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        private Vector2 _inputDirection;
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
            var move = _inputDirection * Time.fixedDeltaTime * _speed;
            _rigidBody.MovePosition(_rigidBody.position + move);
        }

        private void LateUpdate()
        {
            _animator.SetFloat("Speed", _inputDirection.magnitude);
            if (_inputDirection.x != 0)
            {
                _spriteRenderer.flipX = _inputDirection.x < 0;
            }
        }

        private void OnMove(InputValue value)
        {
            _inputDirection = value.Get<Vector2>();
        }
    }
}