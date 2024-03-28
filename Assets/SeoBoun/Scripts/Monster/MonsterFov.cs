using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class MonsterFov : MonoBehaviour
{
    // 몬스터의 시야각
    [SerializeField] float range;               // 얼마나 멀리
    [SerializeField] float angle;               // 어느 각도로
    [SerializeField] LayerMask targetMask;      // 타겟 레이어
    [SerializeField] LayerMask obstacleMask;    // 장애물 레이어

    Collider[] colliders = new Collider[10];

    float cosRange;

    private void Awake()
    {
        cosRange = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    private void Update()
    {
        FindTarget();
    }

    public bool FindTarget()
    {
        // 오버랩 실행
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, targetMask);
        for (int i = 0; i < size; i++)
        {
            // 대상까지의 방향벡터 계산
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, dirToTarget) < cosRange)
                continue;

            // 3. 시야안에 있는가(장애물이 있는 경우에는 볼 수 없음)
            // 내 위치에서 방향으로 레이를 쐈는데 장애물에 걸렸다면(단, 탐색물 뒤에 장애물이 있는 경우는 제외)
            float distToTarget = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
            {
                // 장애물이 존재함
                continue;
            }

            // 볼 수 있음
            Debug.DrawRay(transform.position, dirToTarget * distToTarget, Color.red);
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, range);
    }
}
