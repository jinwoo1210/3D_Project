using UnityEngine;

public class ShotGun : Gun
{
    /*
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
    */
}
