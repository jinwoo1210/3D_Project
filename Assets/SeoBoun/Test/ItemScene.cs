using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;

        PlayerStatManager.Inventory.FieldInventory.EnterScene();
        PlayerStatManager.Inventory.FieldInventory.SetUp();

        if(PlayerStatManager.Inventory.playerStat == null)
        {
            PlayerStatManager.Inventory.playerStat = GameObject.FindObjectOfType<PlayerStat>();
        }

        PlayerStatManager.Inventory.playerStat.LevelUp();
        PlayerStatManager.Inventory.playerStat.GameInit();
    }
}
