using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR : Gun
{
    // TODO �� �����Ͽ� �߰��۾� �ʿ�
    // ������ ���̽�����.
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
