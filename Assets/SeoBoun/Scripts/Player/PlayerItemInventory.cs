using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemInventory : MonoBehaviour
{
    // ������ �κ��丮(����Ʈ �� ���� ����)
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


    [SerializeField] int medicalPoint;        // �Ƿ� ����Ʈ
    [SerializeField] int medicalLevel;        // �Ƿ� �賶 ����

    [SerializeField] int foodLevel;           // �ķ� �賶 ����
    [SerializeField] int foodPoint;           // �ķ� ����Ʈ

    [SerializeField] int electPoint;          // ���� ����Ʈ
    [SerializeField] int electLevel;          // ���� �賶 ����
                                              
    [SerializeField] int toolPoint;           // ���� ����Ʈ
    [SerializeField] int toolLevel;           // ���� �賶 ����

    [SerializeField] List<int> maxPoint = new List<int>();      // ������ �ƽ�����Ʈ

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

    // ������ ��������
    public void GetData(GameData data)
    {

    }

    private void SetMaxPoint()
    {
        // max�� ����?
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