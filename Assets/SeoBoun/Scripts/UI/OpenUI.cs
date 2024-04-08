using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    // â�� ���� UI
    [SerializeField] GameObject targetUI;
    [SerializeField] GameObject blocker;

    public void Open()
    {
        Debug.Log(targetUI.name);
        targetUI.SetActive(true);

        if (blocker != null)
            blocker.SetActive(true);
    }

}
