using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Data/Gun")]
public class GunData : ScriptableObject
{
    public AudioClip gunShotClip;
    public AudioClip gunReloadClip;
    public AudioClip gunEmptyClip;
    public AudioClip damagedClip;

    public GunLevelData[] gunLevelData;
}

[Serializable]
public struct GunLevelData
{
    public int damage;          // 데미지
    public float shootSpeed;    // 연사속도
    public int magCapacity;     // 한 탄창의 용량
    public float reloadTime;    // 장전시간
    public float fireDistance;  // 공격 사거리
}