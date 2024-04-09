using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] float range = 5;               // �󸶳� �ָ�
    [SerializeField] float angle;               // ��� ������
    [SerializeField] LayerMask targetMask;      // Ÿ�� ���̾�
    [SerializeField] LayerMask obstacleMask;    // ��ֹ� ���̾�
    [SerializeField] Transform transformPos;    // �÷��̾��� Ʈ������(���� ������ ����)
    Collider[] colliders = new Collider[7];

    [SerializeField] float cosRange;                     // �ܰ�?

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Awake()
    {
        cosRange = Mathf.Cos(60 * 0.5f * Mathf.Deg2Rad);
    }

    public override void Shoot()
    {
        // ���� ���ο��� �߻�
        if (player.equipWeaponIndex == -1 ||    // �÷��̾ �ƹ��͵� ������� �ʾҰų�(-1),
            state == State.Empty)               // ���� ����ִ� ����(Empty)���
            return;                             // ���� ������ �������� �ʰ� ����(return)

        // �� �� �߻��� �� ���� ���� ������� ź�� ���̱�
        curAmmo--;

        if (curAmmo <= 0)       // ���� ���� ��ź�� 0�̸� 
        {
            state = State.Empty;    // ���� ��� �ִ� ����(Empty)�� ��ȯ
        }

        // Ʈ���� ������ Ȱ��ȭ
        Use();

        int size = Physics.OverlapSphereNonAlloc(transformPos.position, range, colliders, targetMask);

        for (int i = 0; i < size; i++)
        {

            // �������� ���⺤�� ���
            Vector3 dirToTarget = (colliders[i].transform.position - transformPos.position).normalized;
            //Debug.Log($"cos : {cosRange}");
            //Debug.Log($"���� �� : {Vector3.Dot(transform.position, dirToTarget)}");
            if ((Vector3.Dot(transform.forward, dirToTarget) < cosRange))               //Vector3.Dot : �ܰ� ���� , cosRange �� ���� ���
            {                                                                           // ��ݹ��� �̿��� �������� Debug;
                Debug.Log("�þ߰� �ۿ� ����");
                continue;
            }


            // 3. �þ߾ȿ� �ִ°�(��ֹ��� �ִ� ��쿡�� �� �� ����)
            // �� ��ġ���� �������� ���̸� ���µ� ��ֹ��� �ɷȴٸ�(��, Ž���� �ڿ� ��ֹ��� �ִ� ���� ����)
            float distToTarget = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (Physics.Raycast(transformPos.position, dirToTarget, distToTarget, obstacleMask))
            {
                //��ֹ��� ������
                Debug.Log("��ֹ��� ������");
                continue;
            }

            Debug.Log("��ֹ��� ���� ����");
            // �� �� ����
            Debug.DrawRay(transform.position, dirToTarget * distToTarget, Color.red, 0.5f);         // ���� �ȿ� ���� Ž��

            // ���� Ÿ���� IDamagable �������̽��� ������ �ִٸ�
            IDamagable target = colliders[i].gameObject.GetComponent<IDamagable>();

            // Ÿ�ٿ��� ���� ������(damage)��ŭ Ÿ���� ���ϰ�,
            target?.TakeHit(damage);
            //Debug.Log("������ ��");

            if (((1 << colliders[i].gameObject.layer) & monsterLayer) != 0)
            {
                // Ÿ�ٿ��� �� ������ ����Ʈ �߻�
                ParticleSystem effect = Instantiate(hitEffect, colliders[i].transform.position, Quaternion.LookRotation(colliders[i].transform.position.normalized));
                effect.transform.parent = colliders[i].transform;
            }
            // ���� �ʴ���, �ѱ����� ȭ������ �׻� ������
            muzzleFlash.Play();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 lookDir = AngleToDir(transform.eulerAngles.y);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + 60f * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - 60f * 0.5f);

        Debug.DrawRay(transform.position, lookDir * range, Color.green);
        Debug.DrawRay(transform.position, rightDir * range, Color.blue);
        Debug.DrawRay(transform.position, leftDir * range, Color.blue);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
