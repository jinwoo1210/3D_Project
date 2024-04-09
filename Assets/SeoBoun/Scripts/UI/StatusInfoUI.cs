using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class StatusInfoUI : BindingUI
{
    protected virtual void Awake()
    {
        base.Awake();
    }

    public void ShowGunInfo(GunData gunData, int level)
    {
        // 건 정보 표기 -> 선택한 Gun에대한 데이터 넣기 + level 관리하기
        // texts["DamageText"].text
        // texts["ShootSpeedText"].text
        // texts["MagCapacityText"].text
        // texts["ReloadTimeText"].text
        // texts["FireDistanceText"].text
    }

    public void ShowStatusInfo()
    {
        // 플레이어 스탯 표기
        texts["HPText"].text = (PlayerStatManager.Inventory.hpLevel + 1).ToString();
        texts["StaminaText"].text = (PlayerStatManager.Inventory.staminaLevel + 1).ToString();
        texts["SpeedText"].text = (PlayerStatManager.Inventory.speedLevel + 1).ToString();

        texts["LevelText"].text = (PlayerStatManager.Inventory.hpLevel + PlayerStatManager.Inventory.staminaLevel + PlayerStatManager.Inventory.speedLevel + 3).ToString();
    }

    public void ShowPackInfo()
    {
        if (PlayerStatManager.Inventory == null)
            return;

        texts["MedicalPoint"].text = PlayerStatManager.Inventory.medicalPoint.ToString();
        texts["MedicalLevel"].text = $"{PlayerStatManager.Inventory.medicalLevel + 1}/{4}";
        texts["FoodPoint"].text = PlayerStatManager.Inventory.foodPoint.ToString();
        texts["FoodLevel"].text = $"{PlayerStatManager.Inventory.foodLevel + 1}/{4}";
        texts["ElectPoint"].text = PlayerStatManager.Inventory.electPoint.ToString();
        texts["ElectLevel"].text = $"{PlayerStatManager.Inventory.electLevel + 1}/{4}";
        texts["ToolPoint"].text = PlayerStatManager.Inventory.toolPoint.ToString();
        texts["ToolLevel"].text = $"{PlayerStatManager.Inventory.toolLevel + 1}/{4}";
    }
}
