using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = "GameProperties", menuName = " Game Properties")]
public class GameProperties : ScriptableObject
{
    public int CurrentLevelIndex;
    public int MaxLevel;
}
