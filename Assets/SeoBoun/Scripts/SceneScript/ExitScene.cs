using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScene : BaseScene
{
    [SerializeField] StatusInfoUI statusInfoUI;
    public override IEnumerator LoadingRoutine()
    {
        PlayerItemInventory.Inventory.FieldInventory.ExitScene();
        yield return null;

        statusInfoUI = FindObjectOfType<StatusInfoUI>();
        statusInfoUI.ShowPackInfo();
    }
}
