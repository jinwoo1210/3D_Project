using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public enum State { Ready, Empty, Shooting, Reloading }    // 발사 준비, 탄창 빔, 재장전 중
public class Gun : MonoBehaviour
{
    // BaseGun
    [SerializeField] protected GunData gunData;
    [SerializeField] protected State gunState;

    [SerializeField] protected Transform muzzlePoint;   // 발사 위치
    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected ParticleSystem bloodEffect;

    [SerializeField] protected LineRenderer bulletLineRenderer;

    [SerializeField] protected int magAmmo;     // 현재 탄창에 남아 있는 탄알
    [SerializeField] protected int ammoRemain;  // 남은 전체 탄알

    protected int curDamage;
    protected float curShootSpeed;
    protected float curFireDistance;
    protected float curReloadSpeed;

    Coroutine fireRoutine;

    public event Action<int> ChangeAmmoRemainEvent;
    public event Action<int> ChangeMagAmmoEvent;

    public int MagAmmo
    {
        get { return magAmmo; }
        set 
        { 
            magAmmo = value;
            ChangeMagAmmoEvent?.Invoke(magAmmo);
        }
    }

    public int AmmoRemain
    {
        get { return ammoRemain; }
        set
        {
            ammoRemain = value;
            ChangeAmmoRemainEvent?.Invoke(ammoRemain);
        }
    }
    
    public GunData GunData { get { return gunData; } }
    public State GunState { get { return gunState; } }

    private void Awake()
    {
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void Start()
    {
        Init();
        gunState = State.Ready;
        ChangeMagAmmoEvent?.Invoke(magAmmo);
        ChangeAmmoRemainEvent?.Invoke(ammoRemain);
    }

    public void Init()
    {
        // TODO... GunLevelData를 불러와서 적용하기
        ammoRemain = gunData.gunLevelData[0].magCapacity * 3;   // 전체 예비 탄창
        magAmmo = gunData.gunLevelData[0].magCapacity;          // 현재 탄창
        curDamage = gunData.gunLevelData[0].damage;             // 레벨 데미지
        curShootSpeed = gunData.gunLevelData[0].shootSpeed;
        curFireDistance = gunData.gunLevelData[0].fireDistance;
        curReloadSpeed = gunData.gunLevelData[0].reloadTime;
    }

    public bool Fire()
    {
        if (gunState == State.Empty)
        {
            // Reload
            Reload();
            return false;
        }

        if (gunState == State.Ready)
        {
            fireRoutine = StartCoroutine(FireRoutine());
            return true;
        }

        return false;
    }

    protected virtual IEnumerator FireRoutine()
    {
        gunState = State.Shooting;

        RaycastHit hit;
        Vector3 hitPosition;

        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out hit, curFireDistance))
        {
            IDamagable target = hit.collider.GetComponent<IDamagable>();

            target?.TakeHit(curDamage);

            hitPosition = hit.point;
        }
        else
        {
            hitPosition = muzzlePoint.position + muzzlePoint.forward * curFireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));

        MagAmmo--;
        if (MagAmmo <= 0)
            gunState = State.Empty;

        yield return new WaitForSeconds(curShootSpeed);

        if (gunState != State.Empty)
            gunState = State.Ready;
    }

    protected virtual IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // ShotEffet 재생
        muzzleFlash.Play();

        // TODO.. 소리 집어넣기

        bulletLineRenderer.SetPosition(0, muzzlePoint.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false;
    }

    public bool Reload()
    {
        // TODO.. 레벨에 따라 바꾸기
        if(gunState == State.Reloading || gunState == State.Shooting || AmmoRemain <= 0 || MagAmmo >= gunData.gunLevelData[0].magCapacity)
        {
            return false;
        }

        StartCoroutine(ReloadRoutine());

        return true;
    }

    IEnumerator ReloadRoutine()
    {
        gunState = State.Reloading;

        // TODO.. 재생소리

        yield return new WaitForSeconds(curReloadSpeed);

        int ammoToFill = gunData.gunLevelData[0].magCapacity - MagAmmo;

        if(AmmoRemain < ammoToFill)
        {
            ammoToFill = AmmoRemain;
        }

        MagAmmo += ammoToFill;
        AmmoRemain -= ammoToFill;

        gunState = State.Ready;
    }

    public void AddMagAmmo(Action<int> ChangeEvent)
    {
        ChangeMagAmmoEvent += ChangeEvent;
    }

    public void AddAmmoRemain(Action<int> ChangeEvent)
    {
        ChangeAmmoRemainEvent += ChangeEvent;
    }

    public void DeleteEvent()
    {
        ChangeMagAmmoEvent = null;
        ChangeAmmoRemainEvent = null;
    }

    public void EventInvoke()
    {
        ChangeMagAmmoEvent?.Invoke(MagAmmo);
        ChangeAmmoRemainEvent?.Invoke(AmmoRemain);
    }
}
