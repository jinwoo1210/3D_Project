using UnityEngine;
using System;
using UnityEngine.UI;

public class UpgradeStatLevel : MonoBehaviour
{
    // Upgrade를 진행하는 스크립트
    // Upgrade에 액션 집어넣기
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
        // 업그레이드 할 수 없을 때 실행
        failImage.gameObject.SetActive(true);
    }

    public void Success()
    {
        successImage.gameObject.SetActive(true);
    }

    public void UpgradeEvent()
    {   // 실제 업그레이드 메소드 실행
        Upgrade?.Invoke();
    }

    public void DeleteEvent()
    {
        Upgrade = null;
    }
}
