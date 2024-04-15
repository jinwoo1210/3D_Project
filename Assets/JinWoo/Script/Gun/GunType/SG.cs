using System.Collections;
using UnityEngine;

public class SG : Gun
{
    public override void Init()
    {
        ammoRemain = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SG].magCapacityLevel].magCapacity * 3;
        magCapacity = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SG].magCapacityLevel].magCapacity;
        magAmmo = magCapacity;
        curDamage = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SG].damageLevel].damage;
        curShootSpeed = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SG].shootSpeedLevel].shootSpeed;
        curFireDistance = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SG].fireDistanceLevel].fireDistance;
        curReloadSpeed = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SG].reloadLevel].reloadTime;
    }

    // ����...
    // TODO �� �����Ͽ� �߰��۾� �ʿ�
    // ������ ���̽�����.
    protected override IEnumerator FireRoutine()
    {
        gunState = GunState.Shooting;

        Manager.Sound.PlaySFX(gunData.gunShotClip);

        int parrel = Random.Range(7, 13); // 7 ~ 12�� ���?

        Vector3[] hitPosition = new Vector3[parrel];

        for (int i = 0; i < parrel; i++)
        {
            Vector3 direction = muzzlePoint.forward;
            Vector3 spreadVector = Vector3.zero;

            spreadVector += muzzlePoint.transform.right * Random.Range(-10f, 10f);
            direction += spreadVector.normalized * Random.Range(0f, 0.2f);

            if (Physics.Raycast(muzzlePoint.position, direction, out RaycastHit hit, curFireDistance))
            {
                IDamagable target = hit.collider.gameObject.GetComponent<IDamagable>();

                target?.TakeHit(curDamage);

                hitPosition[i] = hit.point;
            }
            else
            {
                hitPosition[i] = muzzlePoint.position + direction * curFireDistance;
            }
            StartCoroutine(ShotEffect(hitPosition[i], i, parrel));
        }

        MagAmmo--;
        if (MagAmmo <= 0)
            gunState = GunState.Empty;

        yield return new WaitForSeconds(curShootSpeed);

        if (gunState != GunState.Empty)
            gunState = GunState.Ready;
    }

    protected IEnumerator ShotEffect(Vector3 hitPosition, int i, int parrel)
    {
        bulletLineRenderer.positionCount = parrel * 2;

        // ShotEffet ���
        muzzleFlash.Play();

        // TODO.. �Ҹ� ����ֱ� i : 0 -> 0, 1 /i : 1 -> 2, 3 / i : 2 -> 4, 5

        bulletLineRenderer.SetPosition(2 * i, muzzlePoint.position);
        bulletLineRenderer.SetPosition(2 * i + 1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.05f);

        bulletLineRenderer.enabled = false;
    }
}
