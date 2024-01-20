using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public float gameTime;
        public float maxGameTime = 2 * 10f;

        public SpawnManager spawnManager;
        public Player player;

        private void Awake()
        {
            instance = this;
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
    }
}