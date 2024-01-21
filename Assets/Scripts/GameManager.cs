using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("# Game Control")]
        public float gameTime;
        public float maxGameTime = 2 * 10f;
        public bool isAlive;

        [Header("# Player Info")]
        public int health;
        public int maxHealth = 100;
        public int level;
        public int kills;
        public int exp;
        public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

        [Header("# Game Object")]
        public SpawnManager spawnManager;
        public Player player;
        public LevelUp uiLevelUp;

        private void Awake()
        {
            instance = this;
        }

        public void GameStart()
        {
            health = maxHealth;
            uiLevelUp.Select(0);
            isAlive = true;
        }

        void Update()
        {
            if (!isAlive)
            {
                return;
            }
            //TODO: rewrite using coroutine
            gameTime += Time.deltaTime;

            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
            }
        }

        public void GetExp()
        {
            exp++;
            var maxLevel = Mathf.Min(level, nextExp.Length - 1);
            if (exp >= nextExp[maxLevel])
            {
                exp = exp - nextExp[maxLevel];
                level++;
                uiLevelUp.Show();
            }
        }

        public void Stop()
        {
            isAlive = false;
            Time.timeScale = 0;
        }

        public void Resume()
        {
            isAlive = true;
            Time.timeScale = 1;
        }
    }
}