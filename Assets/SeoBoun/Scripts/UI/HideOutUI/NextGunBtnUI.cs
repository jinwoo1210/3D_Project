using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextGunBtnUI : MonoBehaviour
{
    HideScene hide;
    private void Start()
    {
        hide = Manager.Scene.GetCurScene().GetComponent<HideScene>();
    }

    public void NextButton()
    {
        hide.Count++;

        if(hide.Count == 4)
        {
            hide.Count = 0;
        }
    }

    public void PrevButton()
    {
        hide.Count--;

        if(hide.Count == -1)
        {
            hide.Count = 3;
        }
    }
}
