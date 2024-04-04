using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    //public AudioClip shotClip;
    //public AudioClip reloadClip;

    // 총의 상태를 표현하는 타입을 선언
    public enum State { Ready, Empty, Reloading}    // 발사 준비, 탄창 빔, 재장전 중
    public State state { get; private set; }        // 현재 총의 상태

    [SerializeField] Animator animator;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] LayerMask monsterLayer;
    [SerializeField] Player player;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem hitEffect;
    public Transform bulletPos;
    public GameObject bulletObject;
    private Text text;

    public int damage = 25; // 공격력
    public int magCapacity = 25; // 현재 탄창에 남아있는 탄수
    public int ammoReMain = 50; // 남은 전체 탄수

    public float rate = 0.12f; // 연사력
    public float reloadTime = 1.8f; // 장전속도

    public float fireDistance = 100f;  //  사거리 // 플레이어 슈터에 구현되어져 있음.(형식상 구현)

    public void OnFire(InputValue value)
    {
        Debug.Log("Onfire 작동");
        animator.SetTrigger("Fire");
        Shoot();
        player.Attack();
    }

    private void Awake()
    {
        Shoot();
        Text[] texts = GetComponentsInChildren<Text>();
        text = texts[0];
    }

    private void LateUpdate()
    {
        text.text = magCapacity + "/" + ammoReMain;
    }

    public void Shoot()
    {
        magCapacity--;
        muzzleFlash.Play();
        Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red, 0.5f);
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, 100f, monsterLayer))
        {
            IDamagable target = hit.collider.gameObject.GetComponent<IDamagable>();

            target?.TakeHit(damage);
            Debug.Log("몬스터 공격");

            ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform;
        }
    }

    public void Use()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        GameObject instantBullet = Instantiate(bulletObject, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        

        yield return new WaitForSeconds(2f);
        Destroy(instantBullet);
    }

    private void OnReload(InputValue value)
    {
        Reload();
    }

    private void Reload()
    {
        animator.SetTrigger("Reload");
    }
}
