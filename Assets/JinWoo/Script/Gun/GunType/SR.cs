using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR : Gun
{
    // TODO 벽 관련하여 추가작업 필요
    // 벽까지 레이쏴보기.
    protected override IEnumerator FireRoutine()
    {
        gunState = State.Shooting;

        Vector3 hitPosition = Vector3.zero;

        RaycastHit[] hit = Physics.RaycastAll(muzzlePoint.position, muzzlePoint.forward, curFireDistance);

        for (int i = 0; i < hit.Length; i++)
        {
            IDamagable target = hit[i].collider.GetComponent<IDamagable>();

            target?.TakeHit(curDamage);
        }

        hitPosition = muzzlePoint.position + muzzlePoint.forward * curFireDistance;

        StartCoroutine(ShotEffect(hitPosition));

        MagAmmo--;
        if (MagAmmo <= 0)
            gunState = State.Empty;

        yield return new WaitForSeconds(curShootSpeed);

        if (gunState != State.Empty)
            gunState = State.Ready;
    }
}
