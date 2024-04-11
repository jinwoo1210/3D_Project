using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRayger : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] ParticleSystem hitEffect;       // 히트 이펙트(타격 이펙트)
    [SerializeField] GunData data;                       // 건 안의 데미지 불러오기
    [SerializeField] Rigidbody rigid;

    private void OnTriggerEnter(Collider other)
    {
        if (Extension.Contain(layer , other.gameObject.layer))
        {
            IDamagable target = other.gameObject.GetComponent<IDamagable>();

            // 타겟에게 총의 데미지(damage)만큼 타격을 가하고,
            target?.TakeHit(data.datas[0].damage);

            ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.LookRotation(-rigid.velocity));
            effect.transform.parent = other.transform;

            Destroy(hitEffect, 1);
            Destroy(gameObject);
        }
    }

    // 데미지 설정 함수 구현
}
