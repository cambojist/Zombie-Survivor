using System.Collections;
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
        private Collider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private WaitForFixedUpdate _wait;
        private bool _isAlive = true;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playerRb = GameManager.instance.player.GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _wait = new WaitForFixedUpdate();
        }

        void FixedUpdate()
        {
            if (!GameManager.instance.isAlive)
            {
                return;
            }
            if (!_isAlive || _animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                return;
            }
            var direction = _playerRb.position - _rigidbody.position;
            var moveDirection = Time.fixedDeltaTime * speed * direction.normalized;
            _rigidbody.MovePosition(_rigidbody.position + moveDirection);
            _rigidbody.velocity = Vector2.zero;
        }

        private void LateUpdate()
        {
            if (!GameManager.instance.isAlive)
            {
                return;
            }
            if (!_isAlive) { return; }
            _spriteRenderer.flipX = _playerRb.position.x < _rigidbody.position.x;
        }

        private void OnEnable()
        {
            _playerRb = GameManager.instance.player.GetComponent<Rigidbody2D>();
            _isAlive = true;
            _collider.enabled = true;
            _rigidbody.simulated = true;
            _spriteRenderer.sortingOrder = 2;
            _animator.SetBool("Dead", false);
            health = maxHealth;
        }

        public void Init(SpawnData data)
        {
            var l = animatorController[data.spawnType];
            _animator.runtimeAnimatorController = l;
            speed = data.speed;
            health = data.health;
            maxHealth = data.health;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Bullet") || !_isAlive)
            {
                return;
            }

            health -= collision.GetComponent<Bullet>().damage;
            StartCoroutine(KnockBack());

            if (health > 0)
            {
                _animator.SetTrigger("Hit");
            }
            else
            {
                _isAlive = false;
                _collider.enabled = false;
                _rigidbody.simulated = false;
                _spriteRenderer.sortingOrder = 1;
                _animator.SetBool("Dead", true);
                //exp
                GameManager.instance.kills++;
                GameManager.instance.GetExp();
            }
        }

        private IEnumerator KnockBack()
        {
            yield return _wait;
            var playerPos = GameManager.instance.player.transform.position;
            var direction = (transform.position - playerPos).normalized;
            _rigidbody.AddForce(direction * 3, ForceMode2D.Impulse);
        }

        void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}