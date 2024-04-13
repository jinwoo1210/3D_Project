using UnityEngine;

public class AR : Gun
{

    /*
    public override void Shoot()
    {
        // 실제 내부에서 발사
        if (   // 플레이어가 아무것도 장비하지 않았거나(-1),
            state == State.Empty)               // 총이 비어있는 상태(Empty)라면
            return;                             // 밑의 문장을 실행하지 않고 종료(return)


        Vector3 pos = muzzlePoint.forward;
        pos.y = 0f;

        RaycastHit[] hit;

        if ((hit = Physics.RaycastAll(muzzlePoint.position, pos, fireDistance, shootableLayer)) != null)
        {
            // muzzlePoint에서, muzzlePoint 앞 방향으로, 사거리(fireDistance) 만큼 몬스터에게 레이를 쏘겠다
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red);

            for (int i = 0; i < hit.Length; i++)            //AR은 몬스터 최대 2명까지 타격해야함으로 for문을 사용
            {
                if ( i == 2 || hit.Length == 0)             // 예외처리로 2명 보다 더 맞은 객체와, 아무것도 맞지 않았을때는
                {                                           // 아래의 타격 처리가 되면 안되므로 break 처리
                    break;
                }
                Debug.Log(hit[i].collider.name);
                // 만약 타겟이 IDamagable 인터페이스를 가지고 있다면
                IDamagable target = hit[i].collider.gameObject.GetComponent<IDamagable>();
                // 타겟에게 총의 데미지(damage)만큼 타격을 가하고,
                target?.TakeHit(damage);
                if (((1 << hit[i].collider.gameObject.layer) & monsterLayer) != 0)      //레이어가 몬스터라면! 이펙트 발생
                {
                    // 타겟에게 펑 터지는 이펙트 발생
                    ParticleSystem effect = Instantiate(hitEffect, hit[i].point, Quaternion.LookRotation(hit[i].normal));
                    effect.transform.parent = hit[i].transform;
                }
            }
        }
        // 맞지 않더라도, 총구에서 화염구는 항상 나오며
        muzzleFlash.Play();

        // 트레일 렌더러 활성화
        Use();

        // 한 발 발사할 때 마다 현재 사용중인 탄약 줄이기
        curAmmo--;

        if (curAmmo <= 0)       // 만약 현재 잔탄이 0이면 
        {
            state = State.Empty;    // 총이 비어 있는 상태(Empty)로 전환
        }

    }
    */
}
