using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RagDollController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rigid;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Monster monster;

    [SerializeField] List<Collider> ragdollColliders;

    private void Awake()
    {
        ragdollColliders = new List<Collider>();
        Collider[] allCollider = GetComponentsInChildren<Collider>();
        Collider[] myCollider = GetComponents<Collider>();

        foreach(Collider collider in allCollider)
        {
            if(!myCollider.Contains(collider))
            {
                ragdollColliders.Add(collider);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(RagdollRoutine());
    }

    IEnumerator RagdollRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        animator.enabled = false;
        animator.avatar = null;
        rigid.velocity = Vector3.zero;
        rigid.useGravity = false;
        agent.enabled = false;
        for(int i = 0; i < ragdollColliders.Count; i++)
        {
            // ragdollColliders[i].isTrigger = true;
        }
    }
}
