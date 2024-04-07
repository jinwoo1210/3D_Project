using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenIncreaseWindowUI : MonoBehaviour
{
    // 강화창 띄우는 UI
    [SerializeField] GameObject targetUI;
    [SerializeField] GameObject blocker;

    public void Open()
    {
        targetUI.SetActive(true);

        if (blocker != null)
            blocker.SetActive(true);
    }

}
