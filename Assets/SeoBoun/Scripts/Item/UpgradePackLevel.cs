using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePackLevel : MonoBehaviour
{
    public event Action Upgrade;

    [SerializeField] Image failImage;
    [SerializeField] Image successImage;
    public void UpgradeMedicalLevel()
    {
        if(PlayerStatManager.Inventory.IsLevelUp(ItemType.Medical))
        {
            PlayerStatManager.Inventory.LevelUp(ItemType.Medical);
            Success();
        }
        else
        {
            Fail();
        }
    }

    public void UpgradeFoodLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(ItemType.Food))
        {
            PlayerStatManager.Inventory.LevelUp(ItemType.Food);
            Success();
        }
        else
        {
            Fail();
        }
    }

    public void UpgradeElectLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(ItemType.Elect))
        {
            PlayerStatManager.Inventory.LevelUp(ItemType.Elect);
            Success();
        }
        else
        {
            Fail();
        }
    }

    public void UpgradeToolLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(ItemType.Tool))
        {
            PlayerStatManager.Inventory.LevelUp(ItemType.Tool);
            Success();
        }
        else
        {
            Fail();
        }
    }

    public void Fail()
    {
        failImage.gameObject.SetActive(true);
    }

    public void Success()
    {
        successImage.gameObject.SetActive(true);
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
