using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR : Gun
{
    // TODO 벽 관련하여 추가작업 필요
    // 벽까지 레이쏴보기.
    public override void Init()
    {
        ammoRemain = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SR].magCapacityLevel].magCapacity * 3;
        magCapacity = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SR].magCapacityLevel].magCapacity;
        magAmmo = magCapacity;
        curDamage = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SR].damageLevel].damage;
        curShootSpeed = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SR].shootSpeedLevel].shootSpeed;
        curFireDistance = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SR].fireDistanceLevel].fireDistance;
        curReloadSpeed = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SR].reloadLevel].reloadTime;
    }

    protected override IEnumerator FireRoutine()
    {
        gunState = GunState.Shooting;

        Vector3 hitPosition = Vector3.zero;

        RaycastHit[] hit = Physics.RaycastAll(muzzlePoint.position, muzzlePoint.forward, curFireDistance);

        for (int i = 0; i < hit.Length; i++)
        {
            IDamagable target = hit[i].collider.GetComponent<IDamagable>();

            target?.TakeHit(curDamage);
            ParticleSystem effect = Instantiate(hitEffect, hit[i].point, Quaternion.LookRotation(hit[i].normal));
            effect.transform.parent = hit[i].transform;
            Destroy(effect, 2f);
        }

        hitPosition = muzzlePoint.position + muzzlePoint.forward * curFireDistance;

        StartCoroutine(ShotEffect(hitPosition));

        MagAmmo--;
        if (MagAmmo <= 0)
            gunState = GunState.Empty;

        yield return new WaitForSeconds(curShootSpeed);

        if (gunState != GunState.Empty)
            gunState = GunState.Ready;
    }
}
