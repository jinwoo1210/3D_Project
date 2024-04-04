using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    //public AudioClip shotClip;
    //public AudioClip reloadClip;

    // 총의 상태를 표현하는 타입을 선언
    public enum State { Ready, Empty, Reloading }    // 발사 준비, 탄창 빔, 재장전 중
    public State state { get; private set; }        // 현재 총의 상태

    [SerializeField] Animator animator;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] LayerMask monsterLayer;
    [SerializeField] Player player;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] GunData gunData;
    public Transform bulletPos;
    public GameObject bulletObject;
    private Text text;

    public int damage = 25; // 공격력
    public int magCapacity = 25; // 현재 탄창에 남아있는 탄수
    public int ammoRemain = 50; // 남은 전체 탄수

    //public float rate = 0.12f; // 연사력
    public float reloadTime = 1.8f; // 장전속도

    private float lastFireTime;
    public float fireDistance = 100f;  //  사거리 // 플레이어 슈터에 구현되어져 있음.(형식상 구현)

    //private void Update()
    //{
    //    Debug.Log(rate);
    //    rate += Time.deltaTime;
    //}

    private void OnEnable()
    {
        ammoRemain = gunData.ammoRemain;

        magCapacity = gunData.magCapacity;

        state = State.Ready;

        lastFireTime = 0;
    }

    public void OnFire(InputValue value)
    {
        if(state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire) // rate -> gunData.timeBatFire
        animator.SetTrigger("Fire");
        Shoot();
    }

    public void Shoot()
    {
        if (player.equipWeaponIndex == -1 || state == State.Empty)
            return;
        Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red, 0.5f);
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, 100f, monsterLayer))
        {
            IDamagable target = hit.collider.gameObject.GetComponent<IDamagable>();

            target?.TakeHit(damage);
            Debug.Log("몬스터 공격");

            ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform;
            muzzleFlash.Play();
            Use();
        }
        magCapacity--;

        if (magCapacity <= 0)
        {
            state = State.Empty;
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
        if (state == State.Reloading || ammoRemain <= 0 || magCapacity >= gunData.magCapacity)
        {
            return;
        }
        StartCoroutine(ReloadRoutine());
        animator.SetTrigger("Reload");
    }

    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;

        // 재장전 소리 재생

        yield return new WaitForSeconds(gunData.reloadTime);        //재장전 하는 처리 쉬기

        Debug.Log($"현재 탄약량 : {magCapacity}, 현재 잔여량 : {gunData.ammoRemain}");

        int ammoToFill = gunData.magCapacity - magCapacity;
        Debug.Log($"채워야 할 량 : {ammoToFill}");

        if (ammoRemain < ammoToFill)                 // 사용가능한 Max 탄창 < 필요해서 끌어쓰는 탄창
        {
            ammoToFill = ammoRemain;
        }

        magCapacity += ammoToFill;

        ammoRemain -= ammoToFill;

        state = State.Ready;
    }

}
