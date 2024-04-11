using UnityEngine;

public class ShotGunRe : Gun
{
    public Transform[] bulletSpawner;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Shoot()
    {
        // ���� ���ο��� �߻�
        if (player.equipWeaponIndex == -1 ||    // �÷��̾ �ƹ��͵� ������� �ʾҰų�(-1),
            state == State.Empty)               // ���� ����ִ� ����(Empty)���
            return;                             // ���� ������ �������� �ʰ� ����(return)

        Vector3 pos = muzzlePoint.forward;
        pos.y = 0f;

        // ���� �ʴ���, �ѱ����� ȭ���� �׻� ������
        muzzleFlash.Play();

        // Ʈ���� ������ Ȱ��ȭ
        for (int i = 0; i < bulletSpawner.Length; i++)      // �����̱⿡ �Ѿ˻����� �迭�� ������.
        {
            // Ʈ������ �׸� �Ѿ��� �����ϰ�
            GameObject instantBullet = Instantiate(bulletObject, bulletSpawner[i].position, bulletSpawner[i].rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();

            // �ش� �Ѿ˿��� �� �������� �����ֱ�
            bulletRigid.velocity = bulletSpawner[i].forward * 50;
        }

        // �� �� �߻��� �� ���� ���� ������� ź�� ���̱�
        curAmmo--;

        if (curAmmo <= 0)       // ���� ���� ��ź�� 0�̸� 
        {
            state = State.Empty;    // ���� ��� �ִ� ����(Empty)�� ��ȯ
        }
    }
}
