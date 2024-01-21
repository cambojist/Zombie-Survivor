using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HUD : MonoBehaviour
    {
        public enum InfoType { Exp, Level, Kill, Time, Health }

        public InfoType type;

        private Text _text;
        private Slider _expBar;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _expBar = GetComponent<Slider>();
        }

        private void LateUpdate()
        {
            switch (type)
            {
                case InfoType.Exp:

                    var exp = GameManager.instance.exp;
                    var maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                    _expBar.value = ((float)exp) / maxExp;
                    break;
                case InfoType.Level:
                    _text.text = string.Format("Level:{0:F0}", GameManager.instance.level);
                    break;
                case InfoType.Kill:
                    _text.text = string.Format("{0:F0}", GameManager.instance.kills);
                    break;
                case InfoType.Time:
                    var remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                    var ts = TimeSpan.FromSeconds(remainTime);
                    _text.text = string.Format("{0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
                    break;
                case InfoType.Health:
                    var health = GameManager.instance.health;
                    var maxHealth = GameManager.instance.maxHealth;
                    _expBar.value = ((float)health) / maxHealth;
                    break;
            }
        }
    }
}