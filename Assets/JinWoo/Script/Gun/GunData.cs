using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Object/GunData")]
public class GunData : ScriptableObject
{
    public enum GunType { AR, SMG, SHOTGUN, SNIPER}     // ������ Ÿ��
    [Header("# Main Info")]

    public GunType gunType;
    public int gunId;       //������ id (�Ѻҷ��ö� ����)
    public string gunName;  //���� �̸� (SMG,Vecter,Sniper ���)
    public string gunDesc;  // ���� ���� ( �ʿ��ϸ� �ְ� �ʿ�X�� ����)

    public int curAmmo;     //���� ���Ǿ����� źâ      

    public Sprite icon;     // �κ� UI ���鶧 ���� ������

    [Header("# Level Data")]
    public LevelData[] datas;

    [Header("# Weapon")]
    public GameObject projectile;   //������ ������ ������
}

[Serializable]
public struct LevelData
{
    // ���Ⱝȭ�� �߰������� ���� ��ġ �Է�
    public int damage;        //������
    public float shootSpeed;    //����ӵ�
    public int remainAmmo;     //źâ�뷮
    public float reloadTime;    //�����ð�
    public float fireDistance;  //���� ��Ÿ�
}