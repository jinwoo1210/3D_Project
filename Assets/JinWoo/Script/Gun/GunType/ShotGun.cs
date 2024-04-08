using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] float range;               // 얼마나 멀리
    [SerializeField] float angle;               // 어느 각도로
    [SerializeField] LayerMask targetMask;      // 타겟 레이어
    [SerializeField] LayerMask obstacleMask;    // 장애물 레이어
    Collider[] colliders = new Collider[7];

    float cosRange;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Awake()
    {
        cosRange = Mathf.Cos(30 * 0.5f * Mathf.Deg2Rad);
    }

    public override void Shoot()
    {
        // 실제 내부에서 발사
        if (player.equipWeaponIndex == -1 ||    // 플레이어가 아무것도 장비하지 않았거나(-1),
            state == State.Empty)               // 총이 비어있는 상태(Empty)라면
            return;                             // 밑의 문장을 실행하지 않고 종료(return)

        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, targetMask);
        for (int i = 0; i < size; i++)
        {

            // 대상까지의 방향벡터 계산
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, dirToTarget) > cosRange)
                continue;

            // 3. 시야안에 있는가(장애물이 있는 경우에는 볼 수 없음)
            // 내 위치에서 방향으로 레이를 쐈는데 장애물에 걸렸다면(단, 탐색물 뒤에 장애물이 있는 경우는 제외)
            float distToTarget = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
            {
                // 장애물이 존재함
                Debug.Log("장애물이 존재함");
                continue;
            }
            else
            {
                Debug.Log("장애물이 존재 안함");
                // 볼 수 있음
                Debug.DrawRay(transform.position, dirToTarget * distToTarget, Color.red);

                // 만약 타겟이 IDamagable 인터페이스를 가지고 있다면
                IDamagable target = colliders[i].gameObject.GetComponent<IDamagable>();

                // 타겟에게 총의 데미지(damage)만큼 타격을 가하고,
                target?.TakeHit(damage);
                Debug.Log("데미지 들어감");

                if (((1 << colliders[i].gameObject.layer) & monsterLayer) != 0)
                {
                    // 타겟에게 펑 터지는 이펙트 발생
                    ParticleSystem effect = Instantiate(hitEffect, colliders[i].transform.position, Quaternion.LookRotation(colliders[i].transform.position.normalized));
                    effect.transform.parent = colliders[i].transform;
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
    }
}
