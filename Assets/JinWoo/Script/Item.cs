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
            // �����ϴ� ���
        }
        else if ( type == Type.Heal)
        {
            // �� �ϴ� ���
        }
        else if (type == Type.Ammo)
        {
            // ź�� ���� ���
        }
    }
}
