using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject String;
    [HideInInspector] public int CurrentArrowCount;
    [SerializeField] private BowConfiguration bowConfiguration;

    private void Awake()
    {
        CurrentArrowCount = bowConfiguration.ArrowCount;
    }
}
