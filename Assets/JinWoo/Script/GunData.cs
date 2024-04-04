using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Object/GunData")]
public class GunData : ScriptableObject
{
    //public enum GunType { }
    [Header("# Main Info")]
    //public GunType gunType;
    public int gunId;       //������ id (�Ѻҷ��ö� ����)
    public string gunName;  //���� �̸� (SMG,Vecter,Sniper ���)
    public string gunDesc;  // ���� ���� ( �ʿ��ϸ� �ְ� �ʿ�X�� ����)
    public int magCapacity = 25;         //���� źâ�� �����ִ� ź��
    public int ammoRemain = 50;          //���� ��ü ź��
    public float timeBetFire = 0.12f;
    public float reloadTime = 1.8f;
    public Sprite icon;     // �κ� UI ���鶧 ���� ������

    [Header("# Level Data")]
    public float BaseDamage = 20;        //������ �⺻ ������
    // ���Ⱝȭ�� �߰������� ���� ��ġ �Է¿��� ( ������, �����)
    public float[] Damage;      //������������ ������
    public float[] ShootSpeed;  //����·����� ������
    [Header("# Weapon")]
    public GameObject projectile;   //������ ������ ������
}
