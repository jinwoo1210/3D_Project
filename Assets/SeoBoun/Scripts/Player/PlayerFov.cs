using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFov : MonoBehaviour
{
    // 시야각에 있는 스포너 비활성화 -> 범위 내의 스포너 활성화 여부 결정

    // 플레이어 범위 구현
    [SerializeField] float activeRange;
    [SerializeField] float disableRange;
    [SerializeField] LayerMask targetMask;

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
            yield return new WaitForSeconds(10f);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, activeRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, disableRange);
    }
}
