using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInfoUI : BindingUI
{
    protected virtual void Awake()
    {
        base.Awake();
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
        // texts["MedicalPoint"].text 
        // texts["MedicalLevel"].text = $"{curLevel}/{maxLevel}"
        // texts["FoodPoint"].text 
        // texts["FoodLevel"].text = $"{curLevel}/{maxLevel}"
        // texts["ElectPoint"].text 
        // texts["ElectLevel"].text = $"{curLevel}/{maxLevel}"
        // texts["ToolPoint"].text 
        // texts["ToolLevel"].text = $"{curLevel}/{maxLevel}"
    }
}
