using UnityEngine;
using System;

public class UpgradeStatLevel : MonoBehaviour
{
    public event Action Upgrade;

    public void UpgradeHpLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(Stats.Hp))
        {
            PlayerStatManager.Inventory.LevelUp(Stats.Hp);
        }
    }

    public void UpgradeStaminaLevel()
    {
        if(PlayerStatManager.Inventory.IsLevelUp(Stats.Stamina))
        {
            PlayerStatManager.Inventory.LevelUp(Stats.Stamina);
        }
    }

    public void UpgradeSpeedLevel()
    {
        if(PlayerStatManager.Inventory.IsLevelUp(Stats.MoveSpeed))
        {
            PlayerStatManager.Inventory.LevelUp(Stats.MoveSpeed);
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
