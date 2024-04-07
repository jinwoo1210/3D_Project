using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemInventory : MonoBehaviour
{
    // 아이템 인벤토리(포인트 및 레벨 관리)
    public static PlayerItemInventory instance;

    public static PlayerItemInventory Inventory { get { return instance; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    [SerializeField] int medicalPoint;        // 의료 포인트
    [SerializeField] int medicalLevel;        // 의료 배낭 레벨

    [SerializeField] int foodLevel;           // 식량 배낭 레벨
    [SerializeField] int foodPoint;           // 식량 포인트

    [SerializeField] int electPoint;          // 전자 포인트
    [SerializeField] int electLevel;          // 전자 배낭 레벨
                                              
    [SerializeField] int toolPoint;           // 도구 포인트
    [SerializeField] int toolLevel;           // 도구 배낭 레벨

    [SerializeField] List<int> maxPoint = new List<int>();      // 레벨별 맥스포인트

    public event Action<int, int> ChangeMedicalPoint;
    public event Action<int, int> ChangeFoodPoint;
    public event Action<int, int> ChangeElectPoint;
    public event Action<int, int> ChangeToolPoint;

    #region property
    public int MedicalPoint
    {
        get { return medicalPoint; }
        set 
        { 
            medicalPoint = Mathf.Clamp(value, 0, maxPoint[medicalLevel]);
            ChangeMedicalPoint?.Invoke(maxPoint[medicalLevel], medicalPoint);
        }
    }

    public int MedicalLevel { get { return medicalLevel; } }

    public void AddMedicalEvent(Action<int, int> action)
    {
        ChangeMedicalPoint += action;
    }

    public int FoodPoint
    {
        get { return foodPoint; }
        set
        {
            foodPoint = Mathf.Clamp(value, 0, maxPoint[foodLevel]);
            ChangeFoodPoint?.Invoke(maxPoint[foodLevel], foodPoint);
        }
    }

    public int FoodLevel { get { return foodLevel; } }

    public void AddFoodEvent(Action<int, int> action)
    {
        ChangeFoodPoint += action;
    }

    public int ElectPoint
    {
        get { return electPoint; }
        set 
        { 
            electPoint = Mathf.Clamp(value, 0, maxPoint[electLevel]);
            ChangeElectPoint?.Invoke(maxPoint[electLevel], electPoint);
        }
    }

    public int ElectLevel { get { return electLevel; } }

    public void AddElectEvent(Action<int, int> action)
    {
        ChangeElectPoint += action;
    }

    public int ToolPoint
    {
        get { return toolPoint; }
        set
        { 
            toolPoint = Mathf.Clamp(value, 0, maxPoint[toolLevel]);
            ChangeToolPoint?.Invoke(maxPoint[toolLevel], toolPoint);
        }
    }

    public int ToolLevel { get { return toolLevel; } }

    public void AddToolEvent(Action<int, int> action)
    {
        ChangeToolPoint += action;
    }
    #endregion

    public void GetItem(ItemType type, int cost)
    {
        switch(type)
        {
            case ItemType.Medical:
                MedicalPoint += cost;
                break;
            case ItemType.Food:
                FoodPoint += cost;
                break;
            case ItemType.Elect:
                ElectPoint += cost;
                break;
            case ItemType.Tool:
                ToolPoint += cost;
                break;
        }
    }
    
    public void UsePoint(ItemType type, int cost)
    {
        switch (type)
        {
            case ItemType.Medical:
                MedicalPoint -= cost;
                break;
            case ItemType.Food:
                FoodPoint -= cost;
                break;
            case ItemType.Elect:
                ElectPoint -= cost;
                break;
            case ItemType.Tool:
                ToolPoint -= cost;
                break;
        }
    }

    // 데이터 가져오기
    public void GetData(GameData data)
    {

    }

    private void SetMaxPoint()
    {
        // max값 설정?
        maxPoint.Add(5);    // 0
        maxPoint.Add(10);   // 1
        maxPoint.Add(15);   // 2
        maxPoint.Add(20);   // 3
        maxPoint.Add(25);   // 4
        maxPoint.Add(30);   // 5
    }

    public bool isFull(ItemType type)
    {
        switch (type)
        {
            case ItemType.Medical:
                if (MedicalPoint == maxPoint[medicalLevel]) return true;
                break;
            case ItemType.Food:
                if (FoodPoint == maxPoint[foodLevel]) return true;
                break;
            case ItemType.Elect:
                if (ElectPoint == maxPoint[electLevel]) return true;
                break;
            case ItemType.Tool:
                if (ToolPoint == maxPoint[toolLevel]) return true;
                break;
        }
        return false;
    }

    public int curPoint(ItemType type)
    {
        switch(type)
        {
            case ItemType.Medical:
                return MedicalPoint;
            case ItemType.Food:
                return FoodPoint;
            case ItemType.Elect:
                return ElectPoint;
            case ItemType.Tool:
                return ToolPoint;
        }

        return -1;
    }
}