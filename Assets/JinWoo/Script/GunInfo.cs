using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInfo : MonoBehaviour
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25; // ���ݷ�
    public int magCapacity = 25; // źâ �뷮

    public float timeBetFire = 0.12f; // �����
    public float reloadTime = 1.8f; // �����ӵ�

    private float fireDistance = 100f;  //  ��Ÿ� // �÷��̾� ���Ϳ� �����Ǿ��� ����.(���Ļ� ����)
}
