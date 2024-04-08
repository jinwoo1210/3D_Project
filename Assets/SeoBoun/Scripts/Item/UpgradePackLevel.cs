using System;
using UnityEngine;

public class UpgradePackLevel : MonoBehaviour
{
    public event Action Upgrade;
    public void UpgradeMedicalLevel()
    {
        if(PlayerStatManager.Inventory.IsLevelUp(ItemType.Medical))
        {
            PlayerStatManager.Inventory.LevelUp(ItemType.Medical);
        }
    }

    public void UpgradeFoodLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(ItemType.Food))
        {
            PlayerStatManager.Inventory.LevelUp(ItemType.Food);
        }
    }

    public void UpgradeElectLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(ItemType.Elect))
        {
            PlayerStatManager.Inventory.LevelUp(ItemType.Elect);
        }
    }

    public void UpgradeToolLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(ItemType.Tool))
        {
            PlayerStatManager.Inventory.LevelUp(ItemType.Tool);
        }
    }

    public void UpgradeEvent()
    {
        Upgrade?.Invoke();
    }

    public void DeleteEvent()
    {
        Upgrade = null;
    }
}
