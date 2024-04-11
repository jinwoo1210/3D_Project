using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideScene : BaseScene
{
    [SerializeField] StatusInfoUI statusInfoUI;
    public override IEnumerator LoadingRoutine()
    {
        PlayerStatManager.Inventory.FieldInventory.ExitScene();
        yield return null;
    }

    public void UpdateInfo()
    {
        if(statusInfoUI == null)
        {
            statusInfoUI = FindObjectOfType<StatusInfoUI>();
        }

        statusInfoUI.ShowPackInfo();
        statusInfoUI.ShowStatusInfo();
    }

    public void ExitScene()
    {
        Manager.Scene.LoadScene("GameScene");
    }

}
