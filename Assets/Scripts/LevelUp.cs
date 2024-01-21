using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelUp : MonoBehaviour
    {
        RectTransform rect;
        Item[] items;

        void Awake()
        {
            rect = GetComponent<RectTransform>();
            items = GetComponentsInChildren<Item>(true);
        }

        public void Show()
        {
            Next();
            rect.localScale = Vector3.one;
            GameManager.instance.Stop();
        }

        public void Hide()
        {
            rect.localScale = Vector3.zero;
            GameManager.instance.Resume();
        }

        public void Select(int index)
        {
            items[index].OnClick();
        }

        public void Next()
        {
            foreach (Item item in items)
            {
                item.gameObject.SetActive(false);
            }

            var numbers = new List<int>();
            while (numbers.Count != 3)
            {
                int a = Random.Range(0, items.Length);
                if (!numbers.Contains(a))
                {
                    numbers.Add(a);
                }
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                var randItem = items[numbers[i]];

                if (randItem.level == randItem.data.damages.Length)
                {
                    items[4].gameObject.SetActive(true);
                }
                else
                {
                    randItem.gameObject.SetActive(true);
                }

            }
        }
    }
}