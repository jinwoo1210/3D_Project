using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class FieldInventory : MonoBehaviour
{
    // 필드 인벤토리
    [SerializeField] int medicalPoint;        // 필드 의료 포인트
    [SerializeField] int foodPoint;           // 필드 식량 포인트
    [SerializeField] int electPoint;          // 필드 전자 포인트
    [SerializeField] int toolPoint;           // 필드 도구 포인트

    [SerializeField] Dictionary<ItemType, int> maxPoints;

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
            medicalPoint = Mathf.Clamp(value, 0, maxPoints[ItemType.Medical]);
            ChangeMedicalPoint?.Invoke(maxPoints[ItemType.Medical], medicalPoint);
        }
    }
    public void AddMedicalEvent(Action<int, int> action)
    {
        ChangeMedicalPoint += action;
    }

    public int FoodPoint
    {
        get { return foodPoint; }
        set
        {
            foodPoint = Mathf.Clamp(value, 0, maxPoints[ItemType.Food]);
            ChangeFoodPoint?.Invoke(maxPoints[ItemType.Food], foodPoint);
        }
    }
    public void AddFoodEvent(Action<int, int> action)
    {
        ChangeFoodPoint += action;
    }

    public int ElectPoint
    {
        get { return electPoint; }
        set
        {
            electPoint = Mathf.Clamp(value, 0, maxPoints[ItemType.Elect]);
            ChangeElectPoint?.Invoke(maxPoints[ItemType.Elect], electPoint);
        }
    }
    public void AddElectEvent(Action<int, int> action)
    {
        ChangeElectPoint += action;
    }

    public int ToolPoint
    {
        get { return toolPoint; }
        set
        {
            toolPoint = Mathf.Clamp(value, 0, maxPoints[ItemType.Tool]);
            ChangeToolPoint?.Invoke(maxPoints[ItemType.Tool], toolPoint);
        }
    }
    public void AddToolEvent(Action<int, int> action)
    {
        ChangeToolPoint += action;
    }
    #endregion

    private void Start()
    {
        EnterScene();
    }

    public void EnterScene()
    {
        // 씬에 들어갈 때, 모든 포인트는 0이며 최댓값 설정하기
        foodPoint = electPoint = toolPoint = 0;

        maxPoints = new Dictionary<ItemType, int>();

        maxPoints.Add(ItemType.Medical, PlayerStatManager.Inventory.maxMedicalPoint[PlayerStatManager.Inventory.medicalLevel]);
        maxPoints.Add(ItemType.Food, PlayerStatManager.Inventory.maxFoodPoint[PlayerStatManager.Inventory.foodLevel]);
        maxPoints.Add(ItemType.Elect, PlayerStatManager.Inventory.maxElectPoint[PlayerStatManager.Inventory.electLevel]);
        maxPoints.Add(ItemType.Tool, PlayerStatManager.Inventory.maxToolPoint[PlayerStatManager.Inventory.toolLevel]);

        SetUp();
    }

    public void ExitScene()
    {
        // 씬에서 나올 때 해당 포인트 누적하기
        PlayerStatManager.Inventory.medicalPoint += medicalPoint;
        PlayerStatManager.Inventory.foodPoint += foodPoint;
        PlayerStatManager.Inventory.electPoint += electPoint;
        PlayerStatManager.Inventory.toolPoint += toolPoint;

        // Todo... 플레이어가 탈출하지 못하였을 경우 포인트 누적은 없음
    }

    public void ExitScene_PlayerDie()
    {
        PlayerStatManager.Inventory.medicalPoint += 0;
        PlayerStatManager.Inventory.foodPoint += 0;
        PlayerStatManager.Inventory.electPoint += 0;
        PlayerStatManager.Inventory.toolPoint += 0;
    }

    public void SetUp()
    {
        // UI 세팅하기
        ChangeMedicalPoint?.Invoke(maxPoints[ItemType.Medical], MedicalPoint);
        ChangeFoodPoint?.Invoke(maxPoints[ItemType.Food], FoodPoint);
        ChangeElectPoint?.Invoke(maxPoints[ItemType.Elect], ElectPoint);
        ChangeToolPoint?.Invoke(maxPoints[ItemType.Tool], ToolPoint);
    }

    public void GetItem(ItemType type, int cost)
    {
        switch (type)
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

    public bool isFull(ItemType type)
    {
        switch (type)
        {
            case ItemType.Medical:
                if (MedicalPoint >= maxPoints[ItemType.Medical]) return true;
                break;
            case ItemType.Food:
                if (FoodPoint >= maxPoints[ItemType.Food]) return true;
                break;
            case ItemType.Elect:
                if (ElectPoint >= maxPoints[ItemType.Elect]) return true;
                break;
            case ItemType.Tool:
                if (ToolPoint >= maxPoints[ItemType.Tool]) return true;
                break;
        }
        return false;
    }
}
