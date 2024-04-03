using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioClip shotClip;
    public AudioClip reloadClip;
    public Transform bulletPos;
    public GameObject bullet;

    public int damage = 25; // ���ݷ�
    public int magCapacity = 25; // ���� źâ�� �����ִ� ź��
    public int ammoReMain = 50; // ���� ��ü ź��

    public float rate = 0.12f; // �����
    public float reloadTime = 1.8f; // �����ӵ�

    public float fireDistance = 100f;  //  ��Ÿ� // �÷��̾� ���Ϳ� �����Ǿ��� ����.(���Ļ� ����)


    public void Use()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
    }
}
