using System.Collections;
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

        Vector3 upDirection = (Manager.Game.playerPos.position + Vector3.up * 2 - transform.position).normalized;
        Vector3 leftDirection = (Manager.Game.playerPos.position + Vector3.up * 1 + Vector3.left * 1 - transform.position).normalized;
        Vector3 rightDirection = (Manager.Game.playerPos.position + Vector3.up * 1 + Vector3.right * 1 - transform.position).normalized;

        RaycastHit[] hits;

        hits = Physics.RaycastAll(transform.position, direction, 20f, obstacleLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            targetObject = hits[i].collider.gameObject;
            targetObject.gameObject.SetActive(false);

            StartCoroutine(CheckRoutine());
        }

        hits = Physics.RaycastAll(transform.position, upDirection, 20f, obstacleLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            targetObject = hits[i].collider.gameObject;
            targetObject.gameObject.SetActive(false);

            StartCoroutine(CheckRoutine());
        }
        hits = Physics.RaycastAll(transform.position, leftDirection, 20f, obstacleLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            targetObject = hits[i].collider.gameObject;
            targetObject.gameObject.SetActive(false);

            StartCoroutine(CheckRoutine());
        }
        hits = Physics.RaycastAll(transform.position, rightDirection, 20f, obstacleLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            targetObject = hits[i].collider.gameObject;
            targetObject.gameObject.SetActive(false);

            StartCoroutine(CheckRoutine());
        }

        Debug.DrawLine(transform.position, Manager.Game.playerPos.position, Color.red);
        Debug.DrawLine(transform.position, Manager.Game.playerPos.position + Vector3.up * 2, Color.blue);
        Debug.DrawLine(transform.position, Manager.Game.playerPos.position + Vector3.up * 2 + Vector3.left * 1, Color.green);
        Debug.DrawLine(transform.position, Manager.Game.playerPos.position + Vector3.up * 1 + Vector3.right * 1, Color.black);
    }

    IEnumerator CheckRoutine()
    {
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime;

            if (time > 3f)
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
