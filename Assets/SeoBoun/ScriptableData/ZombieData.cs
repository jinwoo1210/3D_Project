using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie", menuName = "Data/Zombie")]
public class ZombieData : ScriptableObject
{
    [Header("ZombieStat")]
    public int hp;                  // hp 
    public int moveSpeed;           // 이동속도
    public int targetSpeed;         // 추적 속도
    public float findRange;         // 탐색 범위
    public float attackRange;       // 공격 범위   
    public float attackRate;        // 어택 쿨타임? 빈도?
    public int damage;              // 데미지

    public Material zombieMaterial;
}
