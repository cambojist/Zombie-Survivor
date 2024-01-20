using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    //TODO: rewrite spawn manager
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs;
        private List<GameObject>[] pools;

        void Awake()
        {
            pools = new List<GameObject>[prefabs.Length];

            for (int i = 0; i < pools.Length; i++)
            {
                pools[i] = new List<GameObject>();
            }
        }

        public GameObject Get(int index)
        {
            GameObject enemy = null;
            foreach (GameObject go in pools[index])
            {
                if (!go.activeSelf)
                {
                    enemy = go;
                    enemy.SetActive(true);
                    break;
                }
            }

            if (!enemy)
            {
                enemy = Instantiate(prefabs[index], transform);
                pools[index].Add(enemy);
            }

            return enemy;
        }
    }
}