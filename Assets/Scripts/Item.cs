using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    private Image _icon;
    private Text _text;

    private void Awake()
    {
        _icon = GetComponentsInChildren<Image>()[1];
        _icon.sprite = data.icon;

        Text[] texts = GetComponentsInChildren<Text>();
        _text = texts[0];
    }

    private void LateUpdate()
    {
        _text.text = "Lv." + (level + 1);
    }

    public void OnClick()
    {
        switch (data.type)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    var newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    weapon.LevelUp(data.baseDamage * data.damages[level], data.quantities[level]);
                }
                level++;
                break;
            case ItemData.ItemType.Gloves:
            case ItemData.ItemType.Shoes:
                if (level == 0)
                {
                    var newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    var rate = data.damages[level];
                    gear.LevelUp(rate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
