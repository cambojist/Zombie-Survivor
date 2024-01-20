using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("# Game Control")]
        public float gameTime;
        public float maxGameTime = 2 * 10f;

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

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            health = maxHealth;
        }

        void Update()
        {
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

            if (exp >= nextExp[level])
            {
                exp = exp - nextExp[level];
                level++;
            }
        }
    }
}