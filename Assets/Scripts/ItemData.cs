using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
    public class ItemData : ScriptableObject
    {
        public enum ItemType { Melee, Range, Gloves, Shoes, Heal }

        [Header("# Main Info")]
        public int id;
        public string itemName;
        public ItemType type;
        [TextArea]
        public string desciption;
        public Sprite icon;

        [Header("# Main Info")]
        public float baseDamage;
        public int baseQuantity;
        public float[] damages;
        public int[] quantities;

        [Header("# Weapon")]
        public GameObject projectile;
        public Sprite hand;
    }
}