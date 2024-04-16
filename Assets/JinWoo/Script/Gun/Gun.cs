using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    // BaseGun
    [SerializeField] protected GunData gunData;
    [SerializeField] protected GunState gunState;

    [SerializeField] protected Transform muzzlePoint;   // 발사 위치
    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected ParticleSystem bloodEffect;

    [SerializeField] protected LineRenderer bulletLineRenderer;

    [SerializeField] protected int magCapacity; // 한 탄창
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
    public GunState GunState { get { return gunState; } }
    public int MagCapacity { get { return magCapacity; } }

    private void Awake()
    {
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void Start()
    {
        Init();
        ChangeMagAmmoEvent?.Invoke(magAmmo);
        ChangeAmmoRemainEvent?.Invoke(ammoRemain);
    }

    public void SetGun()
    {
        Init();
        ChangeMagAmmoEvent?.Invoke(magAmmo);
        ChangeAmmoRemainEvent?.Invoke(ammoRemain);
    }

    public virtual void Init()
    {

    }

    public bool Fire()
    {
        if (gunState == GunState.Empty)
        {
            // Reload
            Manager.Sound.PlaySFX(gunData.gunEmptyClip);
            //playerAudioSource.PlayOneShot(gunData.gunEmptyClip);
            return false;
        }

        if (gunState != GunState.Reloading && gunState == GunState.Ready)
        {
            fireRoutine = StartCoroutine(FireRoutine());
            return true;
        }

        return false;
    }

    protected virtual IEnumerator FireRoutine()
    {
        gunState = GunState.Shooting;

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
            gunState = GunState.Empty;

        yield return new WaitForSeconds(curShootSpeed);

        if (gunState != GunState.Empty)
            gunState = GunState.Ready;
    }

    protected virtual IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // ShotEffet 재생
        muzzleFlash.Play();

        // TODO.. 소리 집어넣기
        Manager.Sound.PlaySFX(gunData.gunShotClip);
        //playerAudioSource.PlayOneShot(gunData.gunShotClip);

        bulletLineRenderer.SetPosition(0, muzzlePoint.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false;
    }

    public bool Reload()
    {
        // TODO.. 레벨에 따라 바꾸기
        if(gunState == GunState.Reloading || gunState == GunState.Shooting || AmmoRemain <= 0 || MagAmmo >= magCapacity)
        {
            return false;
        }

        StartCoroutine(ReloadRoutine());

        return true;
    }

    IEnumerator ReloadRoutine()
    {
        gunState = GunState.Reloading;

        // TODO.. 재생소리
        Manager.Sound.PlaySFX(gunData.gunReloadClip);
        //playerAudioSource.PlayOneShot(gunData.gunReloadClip);

        yield return new WaitForSeconds(curReloadSpeed);

        int ammoToFill = magCapacity - MagAmmo;

        if(AmmoRemain < ammoToFill)
        {
            ammoToFill = AmmoRemain;
        }

        MagAmmo += ammoToFill;
        AmmoRemain -= ammoToFill;

        gunState = GunState.Ready;
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
