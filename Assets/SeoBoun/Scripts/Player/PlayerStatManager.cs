using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    // 모든 포인트 관리
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

    public int medicalPoint;        // 의료 포인트
    public int medicalLevel;        // 의료 배낭 레벨

    public int foodLevel;           // 식량 배낭 레벨
    public int foodPoint;           // 식량 포인트

    public int electPoint;          // 전자 포인트
    public int electLevel;          // 전자 배낭 레벨
                         
    public int toolPoint;           // 도구 포인트
    public int toolLevel;           // 도구 배낭 레벨

    public int hpLevel;             // 체력 레벨
    public int staminaLevel;        // 스테미나 레벨
    public int speedLevel;          // 스피드 레벨

    public List<int> maxMedicalPoint = new List<int>();      // 레벨별 맥스포인트
    public List<int> maxFoodPoint = new List<int>();
    public List<int> maxElectPoint = new List<int>();
    public List<int> maxToolPoint = new List<int>();

    public PackLevelUpPoint[] packLevelUpPoint;
    public StatLevelUpPoint[] statLevelUpPoint;

    public void UsePoint(ItemType type, int cost)
    {
        // 포인트 사용
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

    public bool IsLevelUp(ItemType type)
    {   // 배낭 레벨 업 가능여부
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

        return false;
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

}

[Serializable]
public struct PackLevelUpPoint
{
    public int electPoint;
    public int toolPoint;
}

[Serializable]
public struct StatLevelUpPoint
{
    public int medicalPoint;
    public int foodPoint;
}