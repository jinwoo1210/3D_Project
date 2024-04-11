using System.Collections;
using UnityEngine;

public class BulletRayger : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] ParticleSystem hitEffect;           // 히트 이펙트(타격 이펙트)
    [SerializeField] Rigidbody rigid;

    [SerializeField] int bulletDamage;

    Coroutine relaseRoutine;

    public void SetDamage(int damage)
    {
        bulletDamage = damage;
    }

    private void Start()
    {
        relaseRoutine = StartCoroutine(Relase());
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable target = other.gameObject.GetComponent<IDamagable>();

        // 타겟에게 총의 데미지(damage)만큼 타격을 가하고,
        if (target != null)
        {
            Debug.Log($"{other.gameObject.name} 에게 {bulletDamage} 만큼의 데미지를 줍니다({gameObject.name})");
        }

        target?.TakeHit(bulletDamage);

        ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.LookRotation(-rigid.velocity));
        effect.transform.parent = other.transform;

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("총알 삭제");

        if (relaseRoutine != null)
            StopCoroutine(relaseRoutine);
    }

    IEnumerator Relase()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
