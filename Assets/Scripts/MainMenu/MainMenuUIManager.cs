using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameProperties properties;
    [SerializeField] private TMP_Text levelText;

    private void Awake()
    {
        levelText.text = properties.CurrentLevelIndex.ToString();
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(properties.CurrentLevelIndex);
    }
}
