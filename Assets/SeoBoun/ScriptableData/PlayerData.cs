using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxHp;         // �⺻�� 100
    public int maxStamina;    // �⺻�� 100
    public int moveSpeed;     // �⺻�� 3
    public int runSpeed;      // �⺻�� moveSpeed * 1.8
}
