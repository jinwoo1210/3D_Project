using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material transparentMaterial;

    GameObject targetObject;

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
            targetObject = hits[i].collider.gameObject;
            targetObject.gameObject.SetActive(false);

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
                StartReset(targetObject);
                break;
            }

            yield return null;
        }
    }

    private void StartReset(GameObject target)
    {
        targetObject.SetActive(true);
    }
}
