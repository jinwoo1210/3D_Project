using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun_Fix : Gun
{
    /*
    [SerializeField] BulletRayger prefab;       // ���� �Ѿ� ������
    [SerializeField] Transform[] spawnPoint;    // �Ѿ� �߻� ��ġ

    [SerializeField] float bulletSpeed;

    protected override void OnEnable()
    {
        // 1. �� ������ �ʱ�ȭ
        Setup(0);

        // 2. �� ���� �ʱ�ȭ(Ready)
        state = State.Ready;

        // 3. �߻� ��� �ʱ�ȭ
        lastFireTime = 0;
    }

    public override void Shoot()
    {
        // �߻� �Լ�
        if (   // �÷��̾ �ƹ��͵� ������� �ʾҰų�(-1),
            state == State.Empty)               // ���� ����ִ� ����(Empty)���
            return;                             // ���� ������ �������� �ʰ� ����(return)
        
        // �߻� �� ���� �ѱ����� 7���� �Ѿ��� ���� �� �ֵ��� ����
        for(int i = 0; i < spawnPoint.Length; i++)
        {
            // 1. ���� ź ����

            //Vector3 bulletPos = spawnPoint[i].position;
            //bulletPos.y = 1f;

            BulletRayger instance = Instantiate(prefab, spawnPoint[i].position, spawnPoint[i].rotation);

            // 2. ������ �� �ֱ�
            Rigidbody instanceRigid = instance.GetComponent<Rigidbody>();
            instanceRigid.velocity = spawnPoint[i].forward * 50;

            // 3. ���� ��󿡰� ������ �ֱ� -> �ҷ�
            instance.SetDamage(damage);
        }

        // muzzleFlash.Play();
        curAmmo--;

        if(curAmmo <= 0)
        {
            state = State.Empty;
        }
    }
    */
}
