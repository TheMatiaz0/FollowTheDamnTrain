using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionButton : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onClick = null;
    public UnityEvent OnClick => onClick;
}
