using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractor
{
    public enum Type { Weapon, Heal, Ammo}
    public Type type;
    public int value;

    private void Update()
    {
        transform.Rotate(Vector3.up * 30 *  Time.deltaTime, Space.World);
    }

    public void Interact()
    {
        if (type == Type.Weapon)
        {
            // 장착하는 기능
        }
        else if ( type == Type.Heal)
        {
            // 힐 하는 기능
        }
        else if (type == Type.Ammo)
        {
            // 탄약 충전 기능
        }
    }
}
