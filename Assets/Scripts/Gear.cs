using UnityEngine;
using static Assets.Scripts.ItemData;

namespace Assets.Scripts
{
    public class Gear : MonoBehaviour
    {
        public ItemType type;
        public float rate;

        public void Init(ItemData data)
        {
            name = data.itemName + " " + data.id;
            transform.parent = GameManager.instance.player.transform;
            transform.localPosition = Vector3.zero;

            type = data.type;
            rate = data.damages[0];
            ApplyGear();
        }

        public void LevelUp(float rate)
        {
            this.rate = rate;
            ApplyGear();
        }

        void ApplyGear()
        {
            switch (type)
            {
                case ItemType.Gloves:
                    RateUp();
                    break;
                case ItemType.Shoes:
                    SpeedUp();
                    break;
            }
        }

        void RateUp()
        {
            Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

            foreach (Weapon weapon in weapons)
            {
                switch (weapon.id)
                {
                    case 0:
                        weapon.speed = 150 + (150 * rate);
                        break;

                    default:
                        weapon.speed = 0.5f * (1f - rate);
                        break;
                }
            }
        }

        void SpeedUp()
        {
            var speed = 3;
            GameManager.instance.player.speed = speed + speed * rate;
        }
    }
}