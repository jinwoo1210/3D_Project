using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    // ��� ����Ʈ ����
    private static PlayerStatManager instance;
    public FieldInventory FieldInventory;
    public PlayerStat playerStat;

    public static PlayerStatManager Inventory { get { return instance; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int medicalPoint;        // �Ƿ� ����Ʈ
    public int medicalLevel;        // �Ƿ� �賶 ����

    public int foodLevel;           // �ķ� �賶 ����
    public int foodPoint;           // �ķ� ����Ʈ

    public int electPoint;          // ���� ����Ʈ
    public int electLevel;          // ���� �賶 ����
                         
    public int toolPoint;           // ���� ����Ʈ
    public int toolLevel;           // ���� �賶 ����

    public int hpLevel;             // ü�� ����
    public int staminaLevel;        // ���׹̳� ����
    public int speedLevel;          // ���ǵ� ����

    public GunStatLevel[] gunStatLevel; // 0 : SMG, 1 : AR, 2 : SG, 3 : SR

    public List<int> maxMedicalPoint = new List<int>();      // ������ �ƽ�����Ʈ
    public List<int> maxFoodPoint = new List<int>();
    public List<int> maxElectPoint = new List<int>();
    public List<int> maxToolPoint = new List<int>();

    public PackLevelUpPoint[] packLevelUpPoint;
    public StatLevelUpPoint[] statLevelUpPoint;
    public GunLevelUpPoint[] gunLevelUpPoint;

    public void UsePoint(ItemType type, int cost)
    {
        // ����Ʈ ���
        switch (type)
        {
            case ItemType.Medical:
                medicalPoint -= cost;
                break;
            case ItemType.Food:
                foodPoint -= cost;
                break;
            case ItemType.Elect:
                electPoint -= cost;
                break;
            case ItemType.Tool:
                toolPoint -= cost;
                break;
        }
    }

    #region backpackLevelUp
    public bool IsLevelUp(ItemType type)
    {   // �賶 ���� �� ���ɿ���
        bool isLevelUp = false;
        switch (type)
        {
            case ItemType.Medical:
                if (medicalLevel == 3)
                    isLevelUp = false;
                else
                {
                    isLevelUp = (packLevelUpPoint[medicalLevel].electPoint <= electPoint) && (packLevelUpPoint[medicalLevel].toolPoint <= toolPoint);
                }
                break;
            case ItemType.Food:
                if (foodLevel == 3)
                    isLevelUp = false;
                else
                {
                    isLevelUp = (packLevelUpPoint[foodLevel].electPoint <= electPoint) && (packLevelUpPoint[foodLevel].toolPoint <= toolPoint);
                }
                break;
            case ItemType.Elect:
                if (electLevel == 3)
                    isLevelUp = false;
                else
                {
                    isLevelUp = (packLevelUpPoint[electLevel].electPoint <= electPoint) && (packLevelUpPoint[electLevel].toolPoint <= toolPoint);
                }
                break;
            case ItemType.Tool:
                if (toolLevel == 3)
                    isLevelUp = false;
                else
                {
                    isLevelUp = (packLevelUpPoint[toolLevel].electPoint <= electPoint) && (packLevelUpPoint[toolLevel].toolPoint <= toolPoint);
                }
                break;
        }
        return isLevelUp;
    }
    public void LevelUp(ItemType type)
    {
        switch(type)
        {
            case ItemType.Medical:
                electPoint -= packLevelUpPoint[medicalLevel].electPoint;
                toolPoint -= packLevelUpPoint[medicalLevel].toolPoint;
                medicalLevel++;
                break;
            case ItemType.Food:
                electPoint -= packLevelUpPoint[foodLevel].electPoint;
                toolPoint -= packLevelUpPoint[foodLevel].toolPoint;
                foodLevel++;
                break;
            case ItemType.Elect:
                electPoint -= packLevelUpPoint[electLevel].electPoint;
                toolPoint -= packLevelUpPoint[electLevel].toolPoint;
                electLevel++;
                break;
            case ItemType.Tool:
                electPoint -= packLevelUpPoint[toolLevel].electPoint;
                toolPoint -= packLevelUpPoint[toolLevel].toolPoint;
                toolLevel++;
                break;
        }
    }
    #endregion
    #region statLevelUp
    public bool IsLevelUp(Stats type)
    {
        bool isLevelUp = false;

        switch(type)
        {
            case Stats.Hp:
                if (hpLevel == 3)
                    isLevelUp = false;
                else
                {
                    isLevelUp = (statLevelUpPoint[hpLevel].foodPoint <= foodPoint) && (statLevelUpPoint[hpLevel].medicalPoint <= medicalPoint);
                }
                break;
            case Stats.Stamina:
                if (staminaLevel == 3)
                    isLevelUp = false;
                else
                {
                    isLevelUp = (statLevelUpPoint[staminaLevel].foodPoint <= foodPoint) && (statLevelUpPoint[staminaLevel].medicalPoint <= medicalPoint);
                }
                break;
            case Stats.MoveSpeed:
                if (speedLevel == 3)
                    isLevelUp = false;
                else
                {
                    isLevelUp = (statLevelUpPoint[speedLevel].foodPoint <= foodPoint) && (statLevelUpPoint[speedLevel].medicalPoint <= medicalPoint);
                }
                break;
        }

        return isLevelUp;
    }

    public void LevelUp(Stats type)
    {
        switch(type)
        {
            case Stats.Hp:
                foodPoint -= statLevelUpPoint[hpLevel].foodPoint;
                medicalPoint -= statLevelUpPoint[hpLevel].medicalPoint;
                hpLevel++;
                break;
            case Stats.Stamina:
                foodPoint -= statLevelUpPoint[staminaLevel].foodPoint;
                medicalPoint -= statLevelUpPoint[staminaLevel].medicalPoint;
                staminaLevel++;
                break;
            case Stats.MoveSpeed:
                foodPoint -= statLevelUpPoint[speedLevel].foodPoint;
                medicalPoint -= statLevelUpPoint[speedLevel].medicalPoint;
                speedLevel++;
                break;
        }
    }
    #endregion
    #region gunLevelUp
    public bool IsLevelUp(WeaponType type)
    {
        int needElectPoint = gunLevelUpPoint[(gunStatLevel[(int)type].totalLevel) / 3].electPoint;
        int needToolPoint = gunLevelUpPoint[(gunStatLevel[(int)type].totalLevel) / 3].toolPoint;

        if(gunStatLevel[(int)type].totalLevel == 15)
        {
            return false;
        }
        else
        {
            return (needElectPoint <= electPoint && needToolPoint <= toolPoint);
        }
    }

    public void LevelUp(WeaponType type)
    {
        int needElectPoint = gunLevelUpPoint[(gunStatLevel[(int)type].totalLevel) / 3].electPoint;
        int needToolPoint = gunLevelUpPoint[(gunStatLevel[(int)type].totalLevel) / 3].toolPoint;

        electPoint -= needElectPoint;
        toolPoint -= needToolPoint;
        GunRandomLevelUp(type);
    }

    private void GunRandomLevelUp(WeaponType type)
    {
        while (true)
        {
            int rand = UnityEngine.Random.Range(0, 5); // dmg, shootSpeed, capacity, reload, fireDistance �� �ϳ�

            if (rand == 0)
            {
                if (gunStatLevel[(int)type].damageLevel == 3)
                    continue;
                gunStatLevel[(int)type].damageLevel++;
                return;
            }
            else if (rand == 1)
            {
                if (gunStatLevel[(int)type].shootSpeedLevel == 3)
                    continue;
                gunStatLevel[(int)type].shootSpeedLevel++;
                return;
            }
            else if (rand == 2)
            {
                if (gunStatLevel[(int)type].magCapacityLevel == 3)
                    continue;
                gunStatLevel[(int)type].magCapacityLevel++;
                return;
            }
            else if (rand == 3)
            {
                if (gunStatLevel[(int)type].reloadLevel == 3)
                    continue;
                gunStatLevel[(int)type].reloadLevel++;
                return;
            }
            else if (rand == 4)
            {
                if (gunStatLevel[(int)type].fireDistanceLevel == 4)
                    continue;
                gunStatLevel[(int)type].fireDistanceLevel++;
                return;
            }
        }
    }
    #endregion
}
