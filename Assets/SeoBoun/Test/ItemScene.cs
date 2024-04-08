using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;

        PlayerItemInventory.Inventory.FieldInventory.EnterScene();
        PlayerItemInventory.Inventory.FieldInventory.SetUp();
    }
}
