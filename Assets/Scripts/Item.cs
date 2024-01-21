using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.ItemData;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    private Image _icon;
    private Text _textLevel;
    private Text _textName;
    private Text _textDesc;

    private void Awake()
    {
        _icon = GetComponentsInChildren<Image>()[1];
        _icon.sprite = data.icon;

        Text[] texts = GetComponentsInChildren<Text>();
        _textLevel = texts[0];
        _textName = texts[1];
        _textDesc = texts[2];

        _textName.text = data.itemName;
    }

    private void OnEnable()
    {
        _textLevel.text = "Lv." + (level + 1);
        switch (data.type)
        {
            case ItemType.Melee:
            case ItemType.Range:
                _textDesc.text = string.Format(data.desciption, data.damages[level] * 100, data.quantities[level]);
                break;
            case ItemType.Gloves:
            case ItemType.Shoes:
                _textDesc.text = string.Format(data.desciption, data.damages[level] * 100);
                break;
            case ItemType.Heal:
                _textDesc.text = string.Format(data.desciption);
                break;
        }
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
