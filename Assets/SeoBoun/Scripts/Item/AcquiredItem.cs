using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquiredItem : BaseItem
{
    public override void Interactor(PlayerInteractor player)
    {
        if (PlayerStatManager.Inventory.FieldInventory.isFull(this.type))
        {
            Debug.Log("Full!");
            return;
        }

        PlayerStatManager.Inventory.FieldInventory.GetItem(this.type, this.cost);

        Destroy(gameObject);
    }
}
