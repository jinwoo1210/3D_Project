using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRayger : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] ParticleSystem hitEffect;       // ��Ʈ ����Ʈ(Ÿ�� ����Ʈ)
    [SerializeField] GunData data;                       // �� ���� ������ �ҷ�����
    [SerializeField] Rigidbody rigid;

    private void OnTriggerEnter(Collider other)
    {
        if (Extension.Contain(layer , other.gameObject.layer))
        {
            IDamagable target = other.gameObject.GetComponent<IDamagable>();

            // Ÿ�ٿ��� ���� ������(damage)��ŭ Ÿ���� ���ϰ�,
            target?.TakeHit(data.datas[0].damage);

            ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.LookRotation(-rigid.velocity));
            effect.transform.parent = other.transform;

            Destroy(hitEffect, 1);
            Destroy(gameObject);
        }
    }

    // ������ ���� �Լ� ����
}
