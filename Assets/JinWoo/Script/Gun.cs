using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25; // ���ݷ�
    public int magCapacity = 25; // ���� źâ�� �����ִ� ź��
    public int ammoReMain = 50; // ���� ��ü ź��

    public float timeBetFire = 0.12f; // �����
    public float reloadTime = 1.8f; // �����ӵ�

    public float fireDistance = 100f;  //  ��Ÿ� // �÷��̾� ���Ϳ� �����Ǿ��� ����.(���Ļ� ����)
}
