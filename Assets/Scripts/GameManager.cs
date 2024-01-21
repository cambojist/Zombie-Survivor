using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public float health;
        public float maxHealth = 100;
        public int level;
        public int kills;
        public int exp;
        public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

        [Header("# Game Object")]
        public SpawnManager spawnManager;
        public Player player;
        public LevelUp uiLevelUp;
        public Result uiResult;
        public GameObject enemyCleaner;

        private void Awake()
        {
            instance = this;
        }

        public void GameStart()
        {
            health = maxHealth;
            uiLevelUp.Select(0);
            Resume();
        }

        public void GameRetry()
        {
            SceneManager.LoadScene(0);
        }

        public void GameOver()
        {
            StartCoroutine(GameOverRoutine());
        }

        IEnumerator GameOverRoutine()
        {
            isAlive = false;
            yield return new WaitForSeconds(0.5f);
            uiResult.gameObject.SetActive(true);
            uiResult.Lose();
            Stop();
        }

        public void GameVictory()
        {
            StartCoroutine(GameVictoryRoutine());
        }

        IEnumerator GameVictoryRoutine()
        {
            isAlive = false;
            yield return new WaitForSeconds(0.5f);
            uiResult.gameObject.SetActive(true);
            uiResult.Win();
            Stop();
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
                GameVictory();
            }
        }

        public void GetExp()
        {
            if (!isAlive)
            {
                return;
            }
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