using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon, Ammo, Medical, Food, Elect, Tool }
public class Item : MonoBehaviour, IInteractor
{
    // 모든 아이템이 가질 베이스 클래스
    public ItemType type;
    public int value;

    private void Update()
    {
        transform.Rotate(Vector3.up * 30 *  Time.deltaTime, Space.World);
    }

    public virtual void Interact()
    {
        Debug.Log("구현하지 않았음!!!");
    }


}
