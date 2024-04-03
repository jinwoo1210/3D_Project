using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerFov : MonoBehaviour
{
    // 플레이어 시야각 구현
    [SerializeField] float angle;
    [SerializeField] float activeRange;
    [SerializeField] float disableRange;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    // 시야각 안에 있는 스포너 비활성화 시켜보기?
    Collider[] colliders = new Collider[20];

    private void Start()
    {
        StartCoroutine(SetSpawner());
    }

    IEnumerator SetSpawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            int size = Physics.OverlapSphereNonAlloc(transform.position, activeRange, colliders, targetMask);
            
            for (int i = 0; i < size; i++)
            {
                if ((transform.position - colliders[i].gameObject.transform.position).sqrMagnitude < disableRange * disableRange)
                {
                    continue;
                }

                ZombieSpanwer target = colliders[i].GetComponent<ZombieSpanwer>();
                target?.StartSpawn();
            }
        }
    }

    // 스포너 조정
    // 2번의 오버랩 시전(5초에 한 번)
    // -> 첫 번째는 활성화 오버랩
    // -> 두 번째는 비활성화 오버랩

    /*
    public void DisableSpawner()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, targetMask);

        for(int i = 0; i < size; i++)
        {
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, dirToTarget) < cosRange) continue;

            float distToTarget = Vector3.Distance(transform.position, colliders[i].transform.position);

            if (Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask)) continue;

            colliders[i].gameObject.SetActive(false);
        }
    }
    */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, activeRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, disableRange);

        /*
        Vector3 lookDir = AngleToDir(transform.eulerAngles.y);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);

        Debug.DrawRay(transform.position, lookDir * range, Color.green);
        Debug.DrawRay(transform.position, rightDir * range, Color.red);
        Debug.DrawRay(transform.position, leftDir * range, Color.red);
        */
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
