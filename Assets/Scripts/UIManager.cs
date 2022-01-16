using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } = null;

    [SerializeField]
    private Text infoText = null;

    protected void Awake()
    {
        Instance = this;
    }

    public void InitializeInteractionInfo(bool activate)
    {
        infoText.gameObject.SetActive(activate);
    }
}
