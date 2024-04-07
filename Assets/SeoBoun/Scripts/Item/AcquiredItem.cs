using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquiredItem : BaseItem
{
    public override void Interactor(PlayerInteractor player)
    {
        if (PlayerItemInventory.Inventory.isFull(this.type))
        {
            Debug.Log("Full!");
            return;
        }

        PlayerItemInventory.Inventory.GetItem(this.type, this.cost);

        Destroy(gameObject);
    }
}
