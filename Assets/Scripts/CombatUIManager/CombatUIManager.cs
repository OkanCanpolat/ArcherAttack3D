using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{
    public static CombatUIManager Instance;
    [SerializeField] private Player player;
    [SerializeField] private GameProperties properties;
    private PlayerAttack playerAttack;
    private Health playerHealth;
    [Header("OnLose")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Image loseResultImage;
    [SerializeField] private TMP_Text loseResultText;
    [SerializeField] private LoseReasonDTO OnArrowFinish;
    [SerializeField] private LoseReasonDTO OnPlayerDied;
    [Header("OnWin")]
    [SerializeField] private GameObject winPanel;
    [Header("Indicators")]
    [SerializeField] private TMP_Text currentArrowIndicator;
    [SerializeField] private TMP_Text currentLevelIndicator;
    [Header("Arrow Hit Description")]
    [SerializeField] private GameObject hitDescriptionPanel;
    [SerializeField] private TMP_Text hitDescription;
    [SerializeField] private Image hitImage;
    [SerializeField] private float hitDescriptionActiveTime;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        playerAttack = player.GetComponent<PlayerAttack>();
        playerHealth = player.GetComponent<Health>();

        playerHealth.OnDie += OnDied;
        playerAttack.OnArrowFinish += OnArrowEnd;
        playerAttack.OnArrowCountChanged += OnArrowCountChanged;
        playerAttack.OnEnemyFinish += OnLevelComplete;

        currentLevelIndicator.text = properties.CurrentLevelIndex.ToString();
    }

    private void OnDied()
    {
        losePanel.SetActive(true);
        loseResultImage.sprite = OnPlayerDied.LoseSprite;
        loseResultText.text = OnPlayerDied.LoseText;
    }
    private void OnArrowEnd()
    {
        losePanel.SetActive(true);
        loseResultImage.sprite = OnArrowFinish.LoseSprite;
        loseResultText.text = OnArrowFinish.LoseText;
    }
    private void OnArrowCountChanged(int value)
    {
        currentArrowIndicator.text = value.ToString();
    }

    private void OnLevelComplete()
    {
        winPanel.SetActive(true);
    }

    public void ActivateHitInfo(BodyPartHitInformation info)
    {
        StartCoroutine(HitInfoActivasion(info));
    }

    private IEnumerator HitInfoActivasion(BodyPartHitInformation info)
    {
        hitDescriptionPanel.SetActive(true);
        hitImage.sprite = info.HitSprite;
        hitDescription.text = info.HitDescription;
        yield return new WaitForSeconds(hitDescriptionActiveTime);
        hitDescriptionPanel.SetActive(false);
    }
}
