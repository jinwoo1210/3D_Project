using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    // Ã¢À» ¶ç¿ì´Â UI
    [SerializeField] protected GameObject targetUI;
    [SerializeField] protected GameObject blocker;

    public void Open()
    {
        Debug.Log(targetUI.name);
        targetUI.SetActive(true);

        if (blocker != null)
            blocker.SetActive(true);
    }

}
