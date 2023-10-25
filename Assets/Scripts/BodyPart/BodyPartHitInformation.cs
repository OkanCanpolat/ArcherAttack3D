using UnityEngine;

[CreateAssetMenu (fileName = "BodyPartHitInformation")]
public class BodyPartHitInformation : ScriptableObject
{
    public BodyPart BodyPart;
    public Sprite HitSprite;
    public string HitDescription;
}
