using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;
        [SerializeField] private RuntimeAnimatorController[] animatorController;

        private Rigidbody2D _playerRb;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private bool _isAlive = true;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playerRb = GameManager.instance.player.GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            if (!_isAlive) { return; }
            var direction = _playerRb.position - _rigidbody.position;
            var moveDirection = Time.fixedDeltaTime * speed * direction.normalized;
            _rigidbody.MovePosition(_rigidbody.position + moveDirection);
            _rigidbody.velocity = Vector2.zero;
        }

        private void LateUpdate()
        {
            if (!_isAlive) { return; }
            _spriteRenderer.flipX = _playerRb.position.x < _rigidbody.position.x;
        }

        private void OnEnable()
        {
            // ?????
            _playerRb = GameManager.instance.player.GetComponent<Rigidbody2D>();
            _isAlive = true;
            health = maxHealth;
        }

        public void Init(SpawnData data)
        {
            Debug.Log(animatorController.Length);
            var l = animatorController[data.spawnType];
            _animator.runtimeAnimatorController = l;
            speed = data.speed;
            health = data.health;
            maxHealth = data.health;
        }
    }
}