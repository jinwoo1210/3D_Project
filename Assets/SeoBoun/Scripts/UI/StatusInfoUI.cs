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
        // TODO... 건 정보 표기
    }

    public void ShowStatusInfo()
    {
        // 플레이어 스탯 표기

        if (PlayerStatManager.Inventory == null)
            return;

        int hpLevel = PlayerStatManager.Inventory.hpLevel;
        int staminaLevel = PlayerStatManager.Inventory.staminaLevel;
        int speedLevel = PlayerStatManager.Inventory.speedLevel;

        texts["LevelText"].text = $"LV. {hpLevel + staminaLevel + speedLevel + 1}";

        // 이미지 표기
        for(int i = 0; i < hpLevel; i++)
        {
            images[$"HPLevel{i + 1}"].gameObject.SetActive(true);
        }
        for (int i = 0; i < staminaLevel; i++)
        {
            images[$"STLevel{i + 1}"].gameObject.SetActive(true);
        }
        for (int i = 0; i < speedLevel; i++)
        {
            images[$"SPLevel{i + 1}"].gameObject.SetActive(true);
        }

    }

    public void ShowPackInfo()
    {
        if (PlayerStatManager.Inventory == null)
            return;

        texts["MedicalPoint"].text = PlayerStatManager.Inventory.medicalPoint.ToString();
        texts["MedicalLevel"].text = $"{PlayerStatManager.Inventory.medicalLevel + 1}";
        texts["FoodPoint"].text = PlayerStatManager.Inventory.foodPoint.ToString();
        texts["FoodLevel"].text = $"{PlayerStatManager.Inventory.foodLevel + 1}";
        texts["ElectPoint"].text = PlayerStatManager.Inventory.electPoint.ToString();
        texts["ElectLevel"].text = $"{PlayerStatManager.Inventory.electLevel + 1}";
        texts["ToolPoint"].text = PlayerStatManager.Inventory.toolPoint.ToString();
        texts["ToolLevel"].text = $"{PlayerStatManager.Inventory.toolLevel + 1}";
    }
}
