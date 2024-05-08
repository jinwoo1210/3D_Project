using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScene : BaseScene
{
    [SerializeField] StatusInfoUI statusInfoUI;
    public override IEnumerator LoadingRoutine()
    {
        yield return null;

        statusInfoUI = FindObjectOfType<StatusInfoUI>();
        statusInfoUI.ShowPackInfo();
        statusInfoUI.ShowStatusInfo();
    }

    public void ShowInfo()
    {
        statusInfoUI.ShowPackInfo();
    }

    public void ShowStatInfo()
    {
        statusInfoUI.ShowStatusInfo();
    }

}
