using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeGunLevel : MonoBehaviour
{
    public event Action Upgrade;
    [SerializeField] Image failImage;
    [SerializeField] Image successImage;

    public void UpgradeSMGLevel()
    {
        if(PlayerStatManager.Inventory.IsLevelUp(WeaponType.SMG))
        {
            PlayerStatManager.Inventory.LevelUp(WeaponType.SMG);
            Success();
        }
        else
        {
            Fail();
        }
    }
    public void UpgradeARLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(WeaponType.AR))
        {
            PlayerStatManager.Inventory.LevelUp(WeaponType.AR);
            Success();
        }
        else
        {
            Fail();
        }
    }
    public void UpgradeSGLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(WeaponType.SG))
        {
            PlayerStatManager.Inventory.LevelUp(WeaponType.SG);
            Success();
        }
        else
        {
            Fail();
        }
    }
    public void UpgradeSRLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(WeaponType.SR))
        {
            PlayerStatManager.Inventory.LevelUp(WeaponType.SR);
            Success();
        }
        else
        {
            Fail();
        }
    }
    public void Fail()
    {
        // ���׷��̵� �� �� ���� �� ����
        failImage.gameObject.SetActive(true);
    }

    public void Success()
    {
        successImage.gameObject.SetActive(true);
    }

    public void UpgradeEvent()
    {   // ���� ���׷��̵� �޼ҵ� ����
        Upgrade?.Invoke();
    }

    public void DeleteEvent()
    {
        Upgrade = null;
    }
}
