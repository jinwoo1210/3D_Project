using System.Collections;
using UnityEngine;

public class BulletRayger : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] ParticleSystem hitEffect;           // ��Ʈ ����Ʈ(Ÿ�� ����Ʈ)
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

        // Ÿ�ٿ��� ���� ������(damage)��ŭ Ÿ���� ���ϰ�,
        if (target != null)
        {
            Debug.Log($"{other.gameObject.name} ���� {bulletDamage} ��ŭ�� �������� �ݴϴ�({gameObject.name})");
        }

        target?.TakeHit(bulletDamage);

        ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.LookRotation(-rigid.velocity));
        effect.transform.parent = other.transform;

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("�Ѿ� ����");

        if (relaseRoutine != null)
            StopCoroutine(relaseRoutine);
    }

    IEnumerator Relase()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
