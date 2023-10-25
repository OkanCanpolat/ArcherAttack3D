using System.Collections.Generic;
using UnityEngine;

public class BodyPartInformationProvider : MonoBehaviour
{
    public static BodyPartInformationProvider Instance;

    [SerializeField] private List<BodyPartHitInformation> hitInformations;
    
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

    }

    public BodyPartHitInformation GetHitPart(BodyPart part)
    {
        foreach(BodyPartHitInformation info in hitInformations)
        {
            if(info.BodyPart == part)
            {
                return info;
            }
        }

        return default;
    }
}
