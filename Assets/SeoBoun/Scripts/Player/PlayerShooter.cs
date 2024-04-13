using Autodesk.Fbx;
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
        if (value.isPressed && !isRoutine && holder.CurEquipGun.GunState != State.Empty)
        {
            isRoutine = true;
            fireStart = StartCoroutine(FireStart());
        }

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
            if (holder.CurEquipGun.GunState == State.Empty)
                break;

            if (holder.CurEquipGun.Fire())
            {
                animator.SetTrigger("Fire");
            }
            yield return null;
        }
    }
}
