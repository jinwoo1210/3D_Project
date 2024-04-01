using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerFov : MonoBehaviour
{
    // 플레이어 시야각 구현
    [SerializeField] float angle;
    [SerializeField] float range;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    // 시야각 안에 있는 스포너 비활성화 시켜보기?
    Collider[] colliders = new Collider[10];

    float cosRange;

    private void Awake()
    {
        cosRange = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    private void Update()
    {
        DisableSpawner();
    }

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
