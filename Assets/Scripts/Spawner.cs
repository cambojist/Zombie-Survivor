using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : MonoBehaviour
    {
        public SpawnData[] spawnData;
        private Transform[] _spawnPoints;
        private float _timer;
        private int level;

        void Start()
        {
            _spawnPoints = GetComponentsInChildren<Transform>();
        }


        void Update()
        {
            //TODO: rewrite using coroutine
            _timer += Time.deltaTime;
            level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

            if (_timer > spawnData[level].spawnTime)
            {
                _timer = 0;
                Spawn();
            }
        }

        private void Spawn()
        {
            GameObject enemy = GameManager.instance.spawnManager.Get(0);
            enemy.transform.position = _spawnPoints[Random.Range(1, _spawnPoints.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnData[level]);
        }
    }

    //TODO: change to Scriptable Object
    [System.Serializable]
    public class SpawnData
    {
        public int spawnType;
        public float spawnTime;
        public int health;
        public float speed;
    }
}