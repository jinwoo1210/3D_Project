using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    //public AudioClip shotClip;
    //public AudioClip reloadClip;

    // 총의 상태를 표현하는 타입을 선언
    public enum State { Ready, Empty, Reloading }    // 발사 준비, 탄창 빔, 재장전 중
    public State state;                              // 현재 총의 상태

    [SerializeField]protected Animator animator;              // 발사 애니메이션
    [SerializeField]protected Transform muzzlePoint;          // 발사 위치(레이캐스트 위치)
    [SerializeField]protected LayerMask shootableLayer;       // 타격 레이어(총을 맞을 수 있는)
    [SerializeField]protected LayerMask monsterLayer;         // 타격 레이어(몬스터)
    [SerializeField]protected LayerMask obstacleLayer;        // 타격 레이어(장애물 / 건물...)
    [SerializeField]protected Player player;                  // 발사할 플레이어
    [SerializeField]protected ParticleSystem muzzleFlash;     // 총구 이펙트(총구 플래시)
    [SerializeField]protected ParticleSystem hitEffect;       // 히트 이펙트(타격 이펙트)
    [SerializeField]protected ParticleSystem bloodEffect;     // 히트 이펙트(피 이펙트)
    [SerializeField]protected GunData gunData;                // 실제 총 데이터 스크립터블 오브젝트
    [SerializeField]protected Transform bulletPos;            // 총알 위치(총구 이펙트 위치)
    [SerializeField]protected GameObject bulletObject;        // 실제 총알 오브젝트

    public int damage;             // 공격력
    public int curAmmo;            // 실제로 사용되어지는 탄약
    public int remainAmmo;         // 장전되어질 탄약
    public int reloadAmmo;         // 재장전시 필요한 탄약
    public float shootSpeed;       // 연사력
    public float reloadTime;       // 장전속도
    public float lastFireTime;    // 마지막 발사 시점 기록용
    public float fireDistance;     // 사거리

    protected virtual void OnEnable()
    {
        // 활성화 초기화
        damage = gunData.datas[0].damage;

        shootSpeed = gunData.datas[0].shootSpeed;

        //실제 사용되어지는 탄창 = 장전해야 할 탄창 
        curAmmo = gunData.datas[0].remainAmmo;

        reloadTime = gunData.datas[0].reloadTime;

        fireDistance = gunData.datas[0].fireDistance;

        // 1. 탄약량 초기화
        remainAmmo = gunData.datas[0].remainAmmo * 3;        // 재장전시 사용되어질 탄약 = GunData.dats[]탄창용량 * 3

        // 2. 총 상태 초기화(Ready)
        state = State.Ready;

        // 3. 발사 기록 초기화
        lastFireTime = 0;
    }

    public void OnFire(InputValue value)
    {
        // 발사 함수

        // 만약 탄창이 비어있다면 Reload 진행
        if (state == State.Empty)
            Reload();
        Debug.Log("발사 if문");
        // 총의 상태가 준비상태(Ready) 이며 이전 발사 시점에서 충분히 지나야 발사 가능
        if (state == State.Ready &&                             // 총이 발사 준비 상태이며,
            Time.time >= lastFireTime + shootSpeed)    // 이전 시점에서 충분히 지난 상태라면(timeBetFire : 발사 간격)
        {
            Debug.Log("발사 1");
            animator.SetTrigger("Fire");    // 발사 트리거 재생
            Shoot();                        // 실제 발사
            lastFireTime = Time.time;       // 마지막 발사 지점 기록(마지막 발사 시점으로부터 timeBetFire만큼 지나면 발사 가능)
        }
    }

    public virtual void Shoot()
    {
        // 실제 내부에서 발사
        if (player.equipWeaponIndex == -1 ||    // 플레이어가 아무것도 장비하지 않았거나(-1),
            state == State.Empty)               // 총이 비어있는 상태(Empty)라면
            return;                             // 밑의 문장을 실행하지 않고 종료(return)

        //Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red, 0.5f);
        
        Vector3 pos = muzzlePoint.forward;
        pos.y = 0f;

        if (Physics.Raycast(muzzlePoint.position, pos, out RaycastHit hit, fireDistance, shootableLayer))
        {
            // muzzlePoint에서, muzzlePoint 앞 방향으로, 사거리(fireDistance) 만큼 몬스터에게 레이를 쏘겠다
            //Debug.Log(hit);

            // 만약 타겟이 IDamagable 인터페이스를 가지고 있다면
            IDamagable target = hit.collider.gameObject.GetComponent<IDamagable>();

            // 타겟에게 총의 데미지(damage)만큼 타격을 가하고,
            target?.TakeHit(damage);

            if (((1 << hit.collider.gameObject.layer) & monsterLayer) != 0)
            {
                // 타겟에게 펑 터지는 이펙트 발생
                ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                effect.transform.parent = hit.transform;
            }
        }
        // 맞지 않더라도, 총구에서 화염구는 항상 나오며
        muzzleFlash.Play();

        // 트레일 렌더러 활성화
        Use();

        // 한 발 발사할 때 마다 현재 사용중인 탄약 줄이기
        curAmmo--;

        if (curAmmo <= 0)       // 만약 현재 잔탄이 0이면 
        {
            state = State.Empty;    // 총이 비어 있는 상태(Empty)로 전환
        }

    }

    public void Use()
    {
        // 트레일 렌더러 활성화
        StartCoroutine("Fire");
    }

    protected IEnumerator Fire()
    {
        // 트레일을 그릴 총알을 생성하고
        GameObject instantBullet = Instantiate(bulletObject, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        // 해당 총알에게 앞 방향으로 가속주기
        bulletRigid.velocity = bulletPos.forward * 50;

        // 2초 뒤에 해당 오브젝트 삭제
        yield return new WaitForSeconds(2f);
        Destroy(instantBullet);
    }

    //protected IEnumerator Blood()
    //{
       
    //}

    protected void OnReload(InputValue value)
    {
        // 재장전
        Reload();
    }

    protected void Reload()
    {
        if (state == State.Reloading ||         // 재장전 상태이며,
            remainAmmo <= 0 ||                  // 재장전시 사용되어질 탄약 0이고,
            curAmmo >= gunData.datas[0].remainAmmo) // 현재 잔탄이 최대일 때
        {
            return;                             // 재장전을 실행하지 않음.
        }

        // 재장전 실행(코루틴, 애니메이션)
        StartCoroutine(ReloadRoutine());
        animator.SetTrigger("Reload");
    }

    protected IEnumerator ReloadRoutine()
    {
        state = State.Reloading;               // 재장전 상태로 전환

        // 재장전 소리 재생

        yield return new WaitForSeconds(reloadTime);        //재장전 하는 처리 쉬기

        Debug.Log($"현재 탄약량 : {curAmmo}, 현재 잔여량 : {remainAmmo}");
        int ammoToFill = gunData.datas[0].remainAmmo - curAmmo;
        Debug.Log($"채워야 할 량 : {ammoToFill}");

        if (remainAmmo < ammoToFill)                 // 사용가능한 Max 탄창 < 필요해서 끌어쓰는 탄창
        {
            ammoToFill = remainAmmo;
        }

        curAmmo += ammoToFill;

        remainAmmo -= ammoToFill;

        state = State.Ready;
    }

    public void Setup(int level)
    {
        damage = gunData.datas[level].damage;

        shootSpeed = gunData.datas[level].shootSpeed;

        curAmmo = gunData.datas[level].remainAmmo;

        reloadTime = gunData.datas[level].reloadTime;

        fireDistance = gunData.datas[level].fireDistance;

        remainAmmo = gunData.datas[level].remainAmmo * 3;        // 재장전시 사용되어질 탄약 = GunData.dats[]탄창용량 * 3

    }
}
