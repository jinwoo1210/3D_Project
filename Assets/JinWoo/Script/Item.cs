using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon, Ammo, Medical, Food, Elect, Tool }
public class Item : MonoBehaviour, IInteractor
{
    // ��� �������� ���� ���̽� Ŭ����
    public ItemType type;
    public int value;

    private void Update()
    {
        transform.Rotate(Vector3.up * 30 *  Time.deltaTime, Space.World);
    }

    public virtual void Interact()
    {
        Debug.Log("�������� �ʾ���!!!");
    }


}
