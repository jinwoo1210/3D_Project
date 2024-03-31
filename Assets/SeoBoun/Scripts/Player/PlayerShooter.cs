using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    // [SerializeField] Gun gun;

    [SerializeField] Animator animator;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] LayerMask monsterLayer;

    bool isRoutine;
    bool isShooting;
    Coroutine fireRoutine;

    private void OnFire(InputValue value)
    {
        animator.SetLayerWeight(1, 1f);
        animator.SetTrigger("Fire");
        isShooting = true;
    
        Shoot();
        if (!isRoutine)
        {
            fireRoutine = StartCoroutine(LayerWeightRoutine());
        }
        isShooting = false;

    }

    IEnumerator LayerWeightRoutine()
    {
        isRoutine = true;
        float time = 0f;
        while (time < 1f)
        {
            if (isShooting)
            {
                time = 0f;
            }
            yield return new WaitForSeconds(0.1f);
            time += 0.1f;
        }
        animator.SetLayerWeight(1, 0f);
        isRoutine = false;
    }

    private void Shoot()
    {
        Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red, 0.5f);
        if(Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, 100f, monsterLayer))
        {
            IDamagable target = hit.collider.gameObject.GetComponent<IDamagable>();

            target?.TakeHit(10);
            Debug.Log("몬스터 공격");
        }
    }
}
