using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInfoUI : BindingUI
{
    protected virtual void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        ShowPackInfo();
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

    public void ShowStatusInfo(PlayerStat player)
    {
        // 플레이어 스탯 표기
        texts["HPText"].text = player.MaxHp.ToString();
        texts["StaminaText"].text = player.MaxStamina.ToString();
        texts["SpeedText"].text = player.MoveSpeed.ToString();
    }

    public void ShowPackInfo()
    {
        texts["MedicalPoint"].text = PlayerItemInventory.Inventory.MedicalPoint.ToString();
        texts["MedicalLevel"].text = $"{PlayerItemInventory.Inventory.MedicalLevel}/{6}";
        texts["FoodPoint"].text = PlayerItemInventory.Inventory.FoodPoint.ToString();
        texts["FoodLevel"].text = $"{PlayerItemInventory.Inventory.FoodLevel}/{6}";
        texts["ElectPoint"].text = PlayerItemInventory.Inventory.ElectPoint.ToString();
        texts["ElectLevel"].text = $"{PlayerItemInventory.Inventory.ElectLevel}/{6}";
        texts["ToolPoint"].text = PlayerItemInventory.Inventory.ToolPoint.ToString();
        texts["ToolLevel"].text = $"{PlayerItemInventory.Inventory.ToolLevel}/{6}";
    }
}
