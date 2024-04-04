using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterNavPoint : MonoBehaviour
{
    // ������ ����޽� ����Ʈ ���
    private void Update()
    {
        GetRandomPointOnNavMesh(transform.position, 1f, NavMesh.AllAreas);
    }

    public void GetRandomPointOnNavMesh(Vector3 center, float distance, int areaMask)
    {
        Vector3 RandomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;

        Debug.Log(NavMesh.SamplePosition(RandomPos, out hit, distance, areaMask));

        // return hit.position;
    }
}
