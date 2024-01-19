using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D targetRb;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private bool _isAlive = true;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!_isAlive) { return; }
        var direction = targetRb.position - _rigidbody.position;
        var moveDirection = Time.fixedDeltaTime * speed * direction.normalized;
        _rigidbody.MovePosition(_rigidbody.position + moveDirection);
        _rigidbody.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!_isAlive) { return; }
        _spriteRenderer.flipX = targetRb.position.x < _rigidbody.position.x;
    }
}
