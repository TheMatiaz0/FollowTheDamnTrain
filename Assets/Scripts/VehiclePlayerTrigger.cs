using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePlayerTrigger : PlayerTrigger
{
    [SerializeField]
    private Transform toppestParent = null;

    public override void OnPlayerTrigger(FPPMovement fpp, bool isEnter)
    {
        if (isEnter)
        {
            fpp.transform.SetParent(toppestParent);
        }

        else
        {
            fpp.transform.SetParent(null);
        }

        fpp.GetComponentInChildren<FPPCamera>().Direction = this.transform.parent.localScale.z;
    }
}
