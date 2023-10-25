using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player Player;
    [SerializeField] private GameProperties properties;
    private Health playerHealth;
    private PlayerAttack playerAttack;
    private bool gameOver;
    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        playerHealth = Player.GetComponent<Health>();
        playerAttack = Player.GetComponent<PlayerAttack>();

        playerHealth.OnDie += OnGameOver;
        playerAttack.OnArrowFinish += OnGameOver;
    }
    private void Start()
    {
        Application.targetFrameRate = 30;
        Time.timeScale = 1;
    }
    public bool IsGameOver()
    {
        return gameOver;
    }
    private void OnGameOver()
    {
        gameOver = true;
    }

    public void OnLevelComplete()
    {
        properties.CurrentLevelIndex++;
        if(properties.CurrentLevelIndex > properties.MaxLevel)
        {
            properties.CurrentLevelIndex = 1;
        }

        MainMennu();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(properties.CurrentLevelIndex);
    }

    public void MainMennu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
