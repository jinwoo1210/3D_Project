using System.Collections;
using UnityEngine;

public class AR : Gun
{
    public override void Init()
    {
        ammoRemain = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.AR].magCapacityLevel].magCapacity * 3;
        magCapacity = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.AR].magCapacityLevel].magCapacity;
        magAmmo = magCapacity;
        curDamage = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.AR].damageLevel].damage;
        curShootSpeed = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.AR].shootSpeedLevel].shootSpeed;
        curFireDistance = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.AR].fireDistanceLevel].fireDistance;
        curReloadSpeed = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.AR].reloadLevel].reloadTime;
    }
    // TODO 벽 관련하여 추가작업 필요
    // 벽까지 레이쏴보기.
    protected override IEnumerator FireRoutine()
    {
        gunState = GunState.Shooting;

        Vector3 hitPosition = Vector3.zero;

        RaycastHit[] hit = Physics.RaycastAll(muzzlePoint.position, muzzlePoint.forward, curFireDistance);

        for(int i = 0; i < hit.Length; i++)
        {
            if (i == 2)
                break;

            IDamagable target = hit[i].collider.GetComponent<IDamagable>();

            target?.TakeHit(curDamage);
        }
        
        if(hit.Length == 0)
        {
            hitPosition = muzzlePoint.position + muzzlePoint.forward * curFireDistance;
        }
        else if(hit.Length == 1)
        {
            hitPosition = hit[0].point;
        }
        else if(hit.Length >= 2)
        {
            hitPosition = hit[1].point;
        }

        StartCoroutine(ShotEffect(hitPosition));

        MagAmmo--;
        if (MagAmmo <= 0)
            gunState = GunState.Empty;

        yield return new WaitForSeconds(curShootSpeed);

        if (gunState != GunState.Empty)
            gunState = GunState.Ready;
    }
}
