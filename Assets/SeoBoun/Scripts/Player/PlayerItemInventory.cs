using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemInventory : MonoBehaviour
{
    // ������ �κ��丮(����Ʈ �� ���� ����)
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

    public int medicalPoint;        // �Ƿ� ����Ʈ
    public int medicalLevel;        // �Ƿ� �賶 ����

    public int foodLevel;           // �ķ� �賶 ����
    public int foodPoint;           // �ķ� ����Ʈ

    public int electPoint;          // ���� ����Ʈ
    public int electLevel;          // ���� �賶 ����
                         
    public int toolPoint;           // ���� ����Ʈ
    public int toolLevel;           // ���� �賶 ����

    public List<int> maxMedicalPoint = new List<int>();      // ������ �ƽ�����Ʈ
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

    // ������ ��������
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