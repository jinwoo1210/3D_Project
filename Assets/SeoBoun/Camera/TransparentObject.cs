using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material transparentMaterial;

    MeshRenderer target;

    bool isReseting = false;

    private void LateUpdate()
    {
        if (Manager.Game.playerPos == null)
            return;

        Vector3 direction = (Manager.Game.playerPos.position - transform.position).normalized;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, 20f, obstacleLayer);

        for(int i = 0; i < hits.Length; i++)
        {
            target = hits[i].collider.GetComponent<MeshRenderer>();
            target.material = transparentMaterial;

            StartCoroutine(CheckRoutine());
        }
    }

    IEnumerator CheckRoutine()
    {
        float time = 0f;

        while(true)
        {
            time += Time.deltaTime;

            if(time > 3f)
            {
                isReseting = true;
                StartReset(target);
                break;
            }

            yield return null;
        }
    }

    private void StartReset(MeshRenderer target)
    {
        target.material = normalMaterial;
    }
}
