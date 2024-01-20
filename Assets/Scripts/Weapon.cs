using Assets.Scripts;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private float _timer;
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        Init();
    }
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(speed * Time.deltaTime * Vector3.back);
                break;
            default:
                _timer += Time.deltaTime;

                if (_timer > speed)
                {
                    _timer = 0;
                    Fire();
                }
                break;
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }
    }

    public void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.spawnManager.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            var rotation = 360 * i * Vector3.forward / count;
            bullet.Rotate(rotation);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count += count;

        if (id == 0)
        {
            Batch();
        }
    }

    private void Fire()
    {
        if (!_player.scanner.nearestTarget)
        {
            return;
        }

        var targetPos = _player.scanner.nearestTarget.position;
        var dir = (targetPos - transform.position).normalized;

        var bullet = GameManager.instance.spawnManager.Get(prefabId).transform;
        bullet.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.up, dir));
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
