using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPRaycastVision : MonoBehaviour
{
    [SerializeField]
    private float maxVisibleDistance = 5;

    protected void Update()
    {
        // layer 8 is reserved for interaction only
        int layerMask = 1 << 8;
        bool isRaycasted;

        if (isRaycasted = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, maxVisibleDistance, layerMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                InteractionButton interaction = hit.transform.parent.GetComponent<InteractionButton>();
                interaction.OnClick.Invoke();
            }
        }

        UIManager.Instance.InitializeInteractionInfo(isRaycasted);
    }
}
