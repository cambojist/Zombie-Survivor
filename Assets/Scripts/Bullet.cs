using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : MonoBehaviour
    {
        public float damage;
        public int per;

        public void Init(float damage, int per)
        {
            this.damage = damage;
            this.per = per;
        }
    }
}