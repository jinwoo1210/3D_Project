using Autodesk.Fbx;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShooter : MonoBehaviour
{
    [SerializeField] WeaponHolder holder;
    [SerializeField] Animator animator;
    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            if(holder.CurEquipGun.Fire())
            {
                animator.SetTrigger("Fire");
            }
        }
    }

    private void OnReload(InputValue value)
    {
        if(holder.CurEquipGun.Reload())
        {
            animator.SetTrigger("Reload");
        }
    }
}
