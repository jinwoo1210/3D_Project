using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerFov : MonoBehaviour
{
    // �÷��̾� �þ߰� ����
    [SerializeField] float angle;
    [SerializeField] float activeRange;
    [SerializeField] float disableRange;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

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

    // ������ ����
    // 2���� ������ ����(5�ʿ� �� ��)
    // -> ù ��°�� Ȱ��ȭ ������
    // -> �� ��°�� ��Ȱ��ȭ ������

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
