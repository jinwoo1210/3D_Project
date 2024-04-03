using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    // [SerializeField] Gun gun;

    [SerializeField] Animator animator;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] LayerMask monsterLayer;
    [SerializeField] Player player;
    [SerializeField] public Gun gun; 
    [SerializeField] ParticleSystem muzzleFlash;

    public void OnFire(InputValue value)
    {
        animator.SetTrigger("Fire");
        Shoot();
        player.Attack();
    }

    private void OnReload(InputValue value)
    {
        Reload();
    }

    public void Shoot()
    {
        muzzleFlash.Play();
        Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red, 0.5f);
        if(Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, 100f, monsterLayer))
        {
            IDamagable target = hit.collider.gameObject.GetComponent<IDamagable>();

            target?.TakeHit(gun.damage);
            Debug.Log("몬스터 공격");
        }
    }

    private void Reload()
    {
        animator.SetTrigger("Reload");
    }

    //public void Attack()
    //{
    //    if (equipWeapon == null)
    //    {
    //        return;
    //    }
    //    fireDelay = Time.deltaTime;
    //    isFireReady = equipWeapon.rate < fireDelay;

    //    if (fDown && isFireReady && !isSwap)
    //    {
    //        Debug.Log("Player 총나가는 이펙트");
    //        equipWeapon.Use();
    //        fireDelay = 0;
    //    }

    //}
}
