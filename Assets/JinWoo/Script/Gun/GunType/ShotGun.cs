using UnityEngine;

public class ShotGun : Gun
{
    /*
    [SerializeField] float range = 5;               // 얼마나 멀리
    [SerializeField] float angle;               // 어느 각도로
    [SerializeField] LayerMask targetMask;      // 타겟 레이어
    [SerializeField] LayerMask obstacleMask;    // 장애물 레이어
    [SerializeField] Transform transformPos;    // 플레이어의 트렌스폼(샤격 판정때 쓰임)
    Collider[] colliders = new Collider[7];

    [SerializeField] float cosRange;                     // 외각?

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
