using Assets.Scripts;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int quantity;
    public float speed;

    private float _timer;
    private Player _player;

    private void Awake()
    {
        _player = GameManager.instance.player;
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

    public void Init(ItemData data)
    {
        name = "Weapon " + data.id;
        transform.parent = _player.transform;
        transform.localPosition = Vector3.zero;

        id = data.id;
        damage = data.baseDamage;
        quantity = data.baseQuantity;

        prefabId = GameManager.instance.spawnManager.FindPrefabId(data.projectile);

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

        var hand = _player.hands[(int)data.type];
        hand.spriteRenderer.sprite = data.hand;
        hand.gameObject.SetActive(true);

        _player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Batch()
    {
        for (int i = 0; i < quantity; i++)
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
            var rotation = 360 * i * Vector3.forward / quantity;
            bullet.Rotate(rotation);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.quantity += count;

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
        bullet.GetComponent<Bullet>().Init(damage, quantity, dir);
    }
}
