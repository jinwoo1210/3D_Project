using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShooter : MonoBehaviour
{
    [SerializeField] WeaponHolder holder;
    [SerializeField] Animator animator;

    Coroutine fireStart;

    bool isRoutine;

    private void OnFire(InputValue value)
    {
        if (value.isPressed && !isRoutine && holder.CurEquipGun.GunState != GunState.Empty)
        {
            isRoutine = true;
            fireStart = StartCoroutine(FireStart());
        }

        //if(value.isPressed == true && holder.CurEquipGun.GunState == GunState.Empty)
        //{
        //    if (holder.CurEquipGun.Reload())
        //    {
        //        animator.SetTrigger("Reload");
        //    }
        //}

        if(value.isPressed == false)
        {
            isRoutine = false;
            StopCoroutine(fireStart);
        }
    }

    private void OnReload(InputValue value)
    {
        if(holder.CurEquipGun.Reload())
        {
            animator.SetTrigger("Reload");
        }
    }

    IEnumerator FireStart()
    {
        while (true)
        {
            if (holder.CurEquipGun.GunState == GunState.Empty)
                break;

            if (holder.CurEquipGun.Fire())
            {
                animator.SetTrigger("Fire");
            }
            yield return null;
        }
    }
}
