using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Cinemachine.DocumentationSortingAttribute;

public class StatusInfoUI : BindingUI
{
    string curType = "SMG";
    string prevType = "SMG";

    protected virtual void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        images[$"SMG"].enabled = false;
        texts[$"SMGTypeText"].enabled = false;
        images[$"AR"].enabled = false;
        texts[$"ARTypeText"].enabled = false;
        images[$"SG"].enabled = false;
        texts[$"SGTypeText"].enabled = false;
        images[$"SR"].enabled = false;
        texts[$"SRTypeText"].enabled = false;
    }

    public void ShowGunInfo(WeaponType type)
    {
        if (PlayerStatManager.Inventory == null)
            return;

        prevType = curType;

        switch(type)
        {
            case WeaponType.SMG:
                curType = "SMG";
                break;
            case WeaponType.AR:
                curType = "AR";
                break;
            case WeaponType.SG:
                curType = "SG";
                break;
            case WeaponType.SR:
                curType = "SR";
                break;
        }

        images[$"{prevType}"].enabled = false;
        texts[$"{prevType}TypeText"].enabled = false;
        texts[$"{curType}TypeText"].enabled = true;
        images[$"{curType}"].enabled = true;

        int DMGLevel = PlayerStatManager.Inventory.gunStatLevel[(int)type].damageLevel;
        int RPMLevel = PlayerStatManager.Inventory.gunStatLevel[(int)type].shootSpeedLevel;
        int CAPLevel = PlayerStatManager.Inventory.gunStatLevel[(int)type].magCapacityLevel;
        int RELOADLevel = PlayerStatManager.Inventory.gunStatLevel[(int)type].reloadLevel;
        int RANGELevel = PlayerStatManager.Inventory.gunStatLevel[(int)type].fireDistanceLevel;

        for (int i = 0; i < 3; i++)
        {
            images[$"DMGLevel{i + 1}"].enabled = false;
            images[$"RPMLevel{i + 1}"].enabled = false;
            images[$"CAPLevel{i + 1}"].enabled = false;
            images[$"RELOADLevel{i + 1}"].enabled = false;
            images[$"RANGELevel{i + 1}"].enabled = false;
        }

        for (int i = 0; i < DMGLevel; i++)
        {
            if (i == 3)
                break;

            images[$"DMGLevel{i + 1}"].enabled = true;
        }
        for (int i = 0; i < RPMLevel; i++)
        {
            if (i == 3)
                break;

            images[$"RPMLevel{i + 1}"].enabled = true;
        }
        for (int i = 0; i < CAPLevel; i++)
        {
            if (i == 3)
                break;

            images[$"CAPLevel{i + 1}"].enabled = true;
        }
        for (int i = 0; i < RELOADLevel; i++)
        {
            if (i == 3)
                break;

            images[$"RELOADLevel{i + 1}"].enabled = true;
        }
        for (int i = 0; i < RANGELevel; i++)
        {
            if (i == 3)
                break;

            images[$"RANGELevel{i + 1}"].enabled = true;
        }
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

        for(int i = 0; i < 3; i++)
        {
            images[$"HPLevel{i + 1}"].enabled = false;
            images[$"STLevel{i + 1}"].enabled = false;
            images[$"SPLevel{i + 1}"].enabled = false;
        }

        // 이미지 표기
        for(int i = 0; i < hpLevel; i++)
        {
            if (i == 3)
                break;

            images[$"HPLevel{i + 1}"].enabled = true;
        }
        for (int i = 0; i < staminaLevel; i++)
        {
            if (i == 3)
                break;

            images[$"STLevel{i + 1}"].enabled = true;
        }
        for (int i = 0; i < speedLevel; i++)
        {
            if (i == 3)
                break;

            images[$"SPLevel{i + 1}"].enabled = true;
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
