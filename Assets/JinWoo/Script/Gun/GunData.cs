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
    public int damage;          // ������
    public float shootSpeed;    // ����ӵ�
    public int magCapacity;     // �� źâ�� �뷮
    public float reloadTime;    // �����ð�
    public float fireDistance;  // ���� ��Ÿ�
}