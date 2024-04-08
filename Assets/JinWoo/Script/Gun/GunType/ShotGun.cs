using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] float range;               // �󸶳� �ָ�
    [SerializeField] float angle;               // ��� ������
    [SerializeField] LayerMask targetMask;      // Ÿ�� ���̾�
    [SerializeField] LayerMask obstacleMask;    // ��ֹ� ���̾�
    Collider[] colliders = new Collider[7];

    float cosRange;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Awake()
    {
        cosRange = Mathf.Cos(30 * 0.5f * Mathf.Deg2Rad);
    }

    public override void Shoot()
    {
        // ���� ���ο��� �߻�
        if (player.equipWeaponIndex == -1 ||    // �÷��̾ �ƹ��͵� ������� �ʾҰų�(-1),
            state == State.Empty)               // ���� ����ִ� ����(Empty)���
            return;                             // ���� ������ �������� �ʰ� ����(return)

        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, targetMask);
        for (int i = 0; i < size; i++)
        {

            // �������� ���⺤�� ���
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, dirToTarget) > cosRange)
                continue;

            // 3. �þ߾ȿ� �ִ°�(��ֹ��� �ִ� ��쿡�� �� �� ����)
            // �� ��ġ���� �������� ���̸� ���µ� ��ֹ��� �ɷȴٸ�(��, Ž���� �ڿ� ��ֹ��� �ִ� ���� ����)
            float distToTarget = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
            {
                // ��ֹ��� ������
                Debug.Log("��ֹ��� ������");
                continue;
            }
            else
            {
                Debug.Log("��ֹ��� ���� ����");
                // �� �� ����
                Debug.DrawRay(transform.position, dirToTarget * distToTarget, Color.red);

                // ���� Ÿ���� IDamagable �������̽��� ������ �ִٸ�
                IDamagable target = colliders[i].gameObject.GetComponent<IDamagable>();

                // Ÿ�ٿ��� ���� ������(damage)��ŭ Ÿ���� ���ϰ�,
                target?.TakeHit(damage);
                Debug.Log("������ ��");

                if (((1 << colliders[i].gameObject.layer) & monsterLayer) != 0)
                {
                    // Ÿ�ٿ��� �� ������ ����Ʈ �߻�
                    ParticleSystem effect = Instantiate(hitEffect, colliders[i].transform.position, Quaternion.LookRotation(colliders[i].transform.position.normalized));
                    effect.transform.parent = colliders[i].transform;
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
    }
}
