using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class MonsterFov : MonoBehaviour
{
    public bool isFind = false;
    
    // ������ �þ߰�
    [SerializeField] float range;               // �󸶳� �ָ�
    [SerializeField] float angle;               // ��� ������
    [SerializeField] LayerMask targetMask;      // Ÿ�� ���̾�
    [SerializeField] LayerMask obstacleMask;    // ��ֹ� ���̾�

    Collider[] colliders = new Collider[10];

    float cosRange;

    Coroutine findRoutine;

    private void Awake()
    {
        cosRange = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    public void StartFind()
    {
        findRoutine = StartCoroutine(FindRoutine());
    }

    private void OnEnable()
    {
        StartFind();
    }

    private void OnDisable()
    {
        StopCoroutine(findRoutine);
    }

    IEnumerator FindRoutine()
    {
        while (true)
        {
            if ((transform.position - Manager.Game.playerPos.position).sqrMagnitude > range * range)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }

            // ������ ����
            int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, targetMask);
            for (int i = 0; i < size; i++)
            {
                // �������� ���⺤�� ���
                Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;

                if (Vector3.Dot(transform.forward, dirToTarget) > cosRange)
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
                isFind = true;
            }
            yield return new WaitForSeconds(1f);
            isFind = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 lookDir = AngleToDir(transform.eulerAngles.y);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);

        Debug.DrawRay(transform.position, lookDir * range, Color.green);
        Debug.DrawRay(transform.position, rightDir * range, Color.red);
        Debug.DrawRay(transform.position, leftDir * range, Color.red);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
