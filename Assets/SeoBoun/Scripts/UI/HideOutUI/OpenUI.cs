using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    // â�� ���� UI
    [SerializeField] protected GameObject targetUI;
    [SerializeField] protected GameObject blocker;

    public void Open()
    {
        targetUI.SetActive(true);

        if (blocker != null)
            blocker.SetActive(true);
    }

}
