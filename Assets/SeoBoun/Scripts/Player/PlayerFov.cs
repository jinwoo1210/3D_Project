using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFov : MonoBehaviour
{
    // �þ߰��� �ִ� ������ ��Ȱ��ȭ -> ���� ���� ������ Ȱ��ȭ ���� ����

    // �÷��̾� ���� ����
    [SerializeField] float activeRange;
    [SerializeField] float disableRange;
    [SerializeField] LayerMask targetMask;

    // �þ߰� �ȿ� �ִ� ������ ��Ȱ��ȭ ���Ѻ���?
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
