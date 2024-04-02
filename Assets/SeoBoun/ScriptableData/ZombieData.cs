using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie", menuName = "Data/Zombie")]
public class ZombieData : ScriptableObject
{
    [Header("ZombieStat")]
    public int hp;                  // hp 
    public int moveSpeed;           // �̵��ӵ�
    public int targetSpeed;         // ���� �ӵ�
    public float findRange;         // Ž�� ����
    public float attackRange;       // ���� ����   
    public float attackRate;        // ���� ��Ÿ��? ��?
    public int damage;              // ������

    // TODO...
    // ���� �ִϸ��̼� ����
    // ���� ���� ����
    // ����Ʈ Ÿ�� ����?
    

}
