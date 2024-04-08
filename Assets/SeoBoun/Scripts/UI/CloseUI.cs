using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUI : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject blocker;

    public void Close()
    {
        target.SetActive(false);

        if (blocker != null)
            blocker.SetActive(false);
    }
}
