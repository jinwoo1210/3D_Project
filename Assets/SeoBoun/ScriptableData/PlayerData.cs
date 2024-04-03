using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxHp;         // 기본값 100
    public int maxStamina;    // 기본값 100
    public int moveSpeed;     // 기본값 3
    public int runSpeed;      // 기본값 moveSpeed * 1.8
}
