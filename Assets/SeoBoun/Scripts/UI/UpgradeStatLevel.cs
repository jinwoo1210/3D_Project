using UnityEngine;
using System;
using UnityEngine.UI;

public class UpgradeStatLevel : MonoBehaviour
{
    // Upgrade�� �����ϴ� ��ũ��Ʈ
    // Upgrade�� �׼� ����ֱ�
    public event Action Upgrade;
    [SerializeField] Image failImage;
    [SerializeField] Image successImage;

    public void UpgradeHpLevel()
    {
        if (PlayerStatManager.Inventory.IsLevelUp(Stats.Hp))
        {
            PlayerStatManager.Inventory.LevelUp(Stats.Hp);
            Success();
        }
        else
        {
            Fail();
        }
    }

    public void UpgradeStaminaLevel()
    {
        if(PlayerStatManager.Inventory.IsLevelUp(Stats.Stamina))
        {
            PlayerStatManager.Inventory.LevelUp(Stats.Stamina);
            Success();
        }
        else
        {
            Fail();
        }
    }

    public void UpgradeSpeedLevel()
    {
        if(PlayerStatManager.Inventory.IsLevelUp(Stats.MoveSpeed))
        {
            PlayerStatManager.Inventory.LevelUp(Stats.MoveSpeed);
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
