using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemInventory : MonoBehaviour
{
    // 아이템 인벤토리(포인트 및 레벨 관리)
    private static PlayerItemInventory instance;
    public FieldInventory FieldInventory;

    public static PlayerItemInventory Inventory { get { return instance; } }

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

    public List<int> maxMedicalPoint = new List<int>();      // 레벨별 맥스포인트
    public List<int> maxFoodPoint = new List<int>();
    public List<int> maxElectPoint = new List<int>();
    public List<int> maxToolPoint = new List<int>();

    public void GetItem(ItemType type, int cost)
    {
        switch(type)
        {
            case ItemType.Medical:
                medicalPoint += cost;
                break;
            case ItemType.Food:
                foodPoint += cost;
                break;
            case ItemType.Elect:
                electPoint += cost;
                break;
            case ItemType.Tool:
                toolPoint += cost;
                break;
        }
    }
    
    public void UsePoint(ItemType type, int cost)
    {
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

    // 데이터 가져오기
    public void SetUp()
    {
        /*
        ChangeMedicalPoint?.Invoke(maxPoint[MedicalLevel], MedicalPoint);
        ChangeFoodPoint?.Invoke(maxPoint[FoodLevel], FoodPoint);
        ChangeElectPoint?.Invoke(maxPoint[ElectLevel], ElectPoint);
        ChangeToolPoint?.Invoke(maxPoint[ToolLevel], ToolPoint);
        */
    }
}