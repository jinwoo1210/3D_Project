using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Scriptable Object/BulletData")]
public class BulletData : ScriptableObject
{
    //public enum BulletType { }
    [Header("# Main Info")]
    //public BulletType bulletType;
    public int bulletId;       //�Ѿ��� id ()
    public string bulletName;  //�Ѿ� �̸� (9ź 5ź ����ź ���)
    public string bulletDesc;  // �Ѿ� ���� ( �ʿ��ϸ� �ְ� �ʿ�X�� ����)
    public int bulletCount;     // �Ѿ� �ֿ����� ����
    public int magCapacity;         //���� źâ�� �����ִ� ź��
    public int ammoRemain;          //���� ��ü ź��
    public Sprite icon;     // �κ� UI ���鶧 ���� ������
    
    [Header("# Weapon")]
    public GameObject projectile;   //�Ѿ��� ������ ������
}
