using UnityEngine;

[CreateAssetMenu (fileName = "LoseReason", menuName = "Lose Reason")]
public class LoseReasonDTO : ScriptableObject
{
    public Sprite LoseSprite;
    public string LoseText;
}
