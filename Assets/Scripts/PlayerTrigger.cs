using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerTrigger : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        FPPMovement player;
        if (player = other.transform.parent.GetComponent<FPPMovement>())
        {
            OnPlayerTrigger(player, true);
        }
    }

    public abstract void OnPlayerTrigger(FPPMovement fpp, bool isEnter);

    protected void OnTriggerExit(Collider other)
    {
        FPPMovement player;
        if (player = other.transform.parent.GetComponent<FPPMovement>())
        {
            OnPlayerTrigger(player, false);
        }
    }
}
