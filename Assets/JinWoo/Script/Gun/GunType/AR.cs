using UnityEngine;

public class AR : Gun
{

    /*
    public override void Shoot()
    {
        // ���� ���ο��� �߻�
        if (   // �÷��̾ �ƹ��͵� ������� �ʾҰų�(-1),
            state == State.Empty)               // ���� ����ִ� ����(Empty)���
            return;                             // ���� ������ �������� �ʰ� ����(return)


        Vector3 pos = muzzlePoint.forward;
        pos.y = 0f;

        RaycastHit[] hit;

        if ((hit = Physics.RaycastAll(muzzlePoint.position, pos, fireDistance, shootableLayer)) != null)
        {
            // muzzlePoint����, muzzlePoint �� ��������, ��Ÿ�(fireDistance) ��ŭ ���Ϳ��� ���̸� ��ڴ�
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red);

            for (int i = 0; i < hit.Length; i++)            //AR�� ���� �ִ� 2����� Ÿ���ؾ������� for���� ���
            {
                if ( i == 2 || hit.Length == 0)             // ����ó���� 2�� ���� �� ���� ��ü��, �ƹ��͵� ���� �ʾ�������
                {                                           // �Ʒ��� Ÿ�� ó���� �Ǹ� �ȵǹǷ� break ó��
                    break;
                }
                Debug.Log(hit[i].collider.name);
                // ���� Ÿ���� IDamagable �������̽��� ������ �ִٸ�
                IDamagable target = hit[i].collider.gameObject.GetComponent<IDamagable>();
                // Ÿ�ٿ��� ���� ������(damage)��ŭ Ÿ���� ���ϰ�,
                target?.TakeHit(damage);
                if (((1 << hit[i].collider.gameObject.layer) & monsterLayer) != 0)      //���̾ ���Ͷ��! ����Ʈ �߻�
                {
                    // Ÿ�ٿ��� �� ������ ����Ʈ �߻�
                    ParticleSystem effect = Instantiate(hitEffect, hit[i].point, Quaternion.LookRotation(hit[i].normal));
                    effect.transform.parent = hit[i].transform;
                }
            }
        }
        // ���� �ʴ���, �ѱ����� ȭ������ �׻� ������
        muzzleFlash.Play();

        // Ʈ���� ������ Ȱ��ȭ
        Use();

        // �� �� �߻��� �� ���� ���� ������� ź�� ���̱�
        curAmmo--;

        if (curAmmo <= 0)       // ���� ���� ��ź�� 0�̸� 
        {
            state = State.Empty;    // ���� ��� �ִ� ����(Empty)�� ��ȯ
        }

    }
    */
}
