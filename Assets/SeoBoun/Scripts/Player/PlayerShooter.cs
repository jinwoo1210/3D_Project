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
    [SerializeField] BulletUI bullet;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem hitEffect;
    

    //public void OnFire(InputValue value)
    //{
    //    animator.SetTrigger("Fire");
    //    Shoot();
    //    player.Attack();
    //}

    //private void OnReload(InputValue value)
    //{
    //    Reload();
    //}

    //public void Shoot()
    //{
    //    bullet.magCapacity--;
    //    muzzleFlash.Play();
    //    Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red, 0.5f);
    //    if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, 100f, monsterLayer))
    //    {
    //        IDamagable target = hit.collider.gameObject.GetComponent<IDamagable>();

    //        target?.TakeHit(gun.damage);
    //        Debug.Log("몬스터 공격");

    //        ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    //        effect.transform.parent = hit.transform;
    //    }
    //}

    //private void Reload()
    //{
    //    animator.SetTrigger("Reload");
    //}
}
