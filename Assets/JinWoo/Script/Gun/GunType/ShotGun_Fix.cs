using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun_Fix : Gun
{
    /*
    [SerializeField] BulletRayger prefab;       // 샷건 총알 프리팹
    [SerializeField] Transform[] spawnPoint;    // 총알 발사 위치

    [SerializeField] float bulletSpeed;

    protected override void OnEnable()
    {
        // 1. 총 데이터 초기화
        Setup(0);

        // 2. 총 상태 초기화(Ready)
        state = State.Ready;

        // 3. 발사 기록 초기화
        lastFireTime = 0;
    }

    public override void Shoot()
    {
        // 발사 함수
        if (   // 플레이어가 아무것도 장비하지 않았거나(-1),
            state == State.Empty)               // 총이 비어있는 상태(Empty)라면
            return;                             // 밑의 문장을 실행하지 않고 종료(return)
        
        // 발사 시 샷건 총구에서 7개의 총알이 나올 수 있도록 설정
        for(int i = 0; i < spawnPoint.Length; i++)
        {
            // 1. 샷건 탄 생성

            //Vector3 bulletPos = spawnPoint[i].position;
            //bulletPos.y = 1f;

            BulletRayger instance = Instantiate(prefab, spawnPoint[i].position, spawnPoint[i].rotation);

            // 2. 앞으로 힘 주기
            Rigidbody instanceRigid = instance.GetComponent<Rigidbody>();
            instanceRigid.velocity = spawnPoint[i].forward * 50;

            // 3. 맞은 대상에게 데미지 주기 -> 불렛
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
