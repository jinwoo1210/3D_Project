using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class MonsterFov : MonoBehaviour
{
    // ������ �þ߰�
    [SerializeField] float range;               // �󸶳� �ָ�
    [SerializeField] float angle;               // ��� ������
    [SerializeField] LayerMask targetMask;      // Ÿ�� ���̾�
    [SerializeField] LayerMask obstacleMask;    // ��ֹ� ���̾�

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
        // ������ ����
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, targetMask);
        for (int i = 0; i < size; i++)
        {
            // �������� ���⺤�� ���
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, dirToTarget) < cosRange)
                continue;

            // 3. �þ߾ȿ� �ִ°�(��ֹ��� �ִ� ��쿡�� �� �� ����)
            // �� ��ġ���� �������� ���̸� ���µ� ��ֹ��� �ɷȴٸ�(��, Ž���� �ڿ� ��ֹ��� �ִ� ���� ����)
            float distToTarget = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
            {
                // ��ֹ��� ������
                continue;
            }

            // �� �� ����
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
