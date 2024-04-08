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
        // �� ���� ǥ�� -> ������ Gun������ ������ �ֱ� + level �����ϱ�
        // texts["DamageText"].text
        // texts["ShootSpeedText"].text
        // texts["MagCapacityText"].text
        // texts["ReloadTimeText"].text
        // texts["FireDistanceText"].text
    }

    public void ShowStatusInfo(PlayerStat player)
    {
        // �÷��̾� ���� ǥ��
        texts["HPText"].text = player.MaxHp.ToString();
        texts["StaminaText"].text = player.MaxStamina.ToString();
        texts["SpeedText"].text = player.MoveSpeed.ToString();
    }

    public void ShowPackInfo()
    {
        if (PlayerItemInventory.Inventory == null)
            return;

        texts["MedicalPoint"].text = PlayerItemInventory.Inventory.medicalPoint.ToString();
        texts["MedicalLevel"].text = $"{PlayerItemInventory.Inventory.medicalLevel}/{6}";
        texts["FoodPoint"].text = PlayerItemInventory.Inventory.foodPoint.ToString();
        texts["FoodLevel"].text = $"{PlayerItemInventory.Inventory.foodLevel}/{6}";
        texts["ElectPoint"].text = PlayerItemInventory.Inventory.electPoint.ToString();
        texts["ElectLevel"].text = $"{PlayerItemInventory.Inventory.electLevel}/{6}";
        texts["ToolPoint"].text = PlayerItemInventory.Inventory.toolPoint.ToString();
        texts["ToolLevel"].text = $"{PlayerItemInventory.Inventory.toolLevel}/{6}";
    }
}
