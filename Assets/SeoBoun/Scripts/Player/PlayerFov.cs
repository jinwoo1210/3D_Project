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
    Collider[] colliders = new Collider[30];
    Collider[] fixedColliders = new Collider[20];

    private void Start()
    {
        StartCoroutine(SetSpawner());
        StartCoroutine(SetFixedSpawner());
    }

    IEnumerator SetSpawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            int size = Physics.OverlapSphereNonAlloc(transform.position, activeRange, colliders, targetMask);
            
            for (int i = 0; i < size; i++)
            {
                if ((transform.position - colliders[i].gameObject.transform.position).sqrMagnitude < disableRange * disableRange)
                {
                    continue;
                }

                SpawnManagement target = colliders[i].GetComponent<SpawnManagement>();
                target?.Spawn();

                FixedSpawner fixedTarget = colliders[i].GetComponent<FixedSpawner>();
                fixedTarget?.Spawn();
            }
        }
    }

    IEnumerator SetFixedSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            int fixedSize = Physics.OverlapSphereNonAlloc(transform.position, activeRange, fixedColliders, targetMask);

            for (int i = 0; i < fixedSize; i++)
            {
                FixedSpawner fixedTarget = fixedColliders[i].GetComponent<FixedSpawner>();
                fixedTarget?.Spawn();
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