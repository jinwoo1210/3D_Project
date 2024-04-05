using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    //public AudioClip shotClip;
    //public AudioClip reloadClip;

    // ���� ���¸� ǥ���ϴ� Ÿ���� ����
    public enum State { Ready, Empty, Reloading }    // �߻� �غ�, źâ ��, ������ ��
    
    public State state;                              // ���� ���� ����
    
    [SerializeField] Animator animator;              // �߻� �ִϸ��̼�
    [SerializeField] Transform muzzlePoint;          // �߻� ��ġ(����ĳ��Ʈ ��ġ)
    [SerializeField] LayerMask shootableLayer;       // Ÿ�� ���̾�(���� ���� �� �ִ�)
    [SerializeField] LayerMask monsterLayer;         // Ÿ�� ���̾�(����)
    [SerializeField] LayerMask obstacleLayer;        // Ÿ�� ���̾�(��ֹ� / �ǹ�...)
    [SerializeField] Player player;                  // �߻��� �÷��̾�
    [SerializeField] ParticleSystem muzzleFlash;     // �ѱ� ����Ʈ(�ѱ� �÷���)
    [SerializeField] ParticleSystem hitEffect;       // ��Ʈ ����Ʈ(Ÿ�� ����Ʈ)
    [SerializeField] ParticleSystem bloodEffect;     // ��Ʈ ����Ʈ(�� ����Ʈ)
    [SerializeField] GunData gunData;                // ���� �� ������ ��ũ���ͺ� ������Ʈ
    [SerializeField] Transform bulletPos;            // �Ѿ� ��ġ(�ѱ� ����Ʈ ��ġ)
    [SerializeField] GameObject bulletObject;        // ���� �Ѿ� ������Ʈ

    public int damage = 25;                          // ���ݷ�
    public int magCapacity = 25;                     // ���� źâ�� �����ִ� ź��
    public int ammoRemain = 50;                      // ���� ��ü ź��

    //public float rate = 0.12f; // �����
    public float reloadTime = 1.8f;                  // �����ӵ�

    private float lastFireTime;                      // ������ �߻� ���� ��Ͽ�
    public float fireDistance = 100f;                // ��Ÿ�

    private void OnEnable()
    {
        // Ȱ��ȭ �ʱ�ȭ

        // 1. ź�෮ �ʱ�ȭ
        ammoRemain = gunData.ammoRemain;        // ammoRemain : ��ü �����ִ� ź��
        magCapacity = gunData.magCapacity;      // magCapacity : ���� źâ�� �����ִ� �Ѿ��� �� -> ���� ���Ǵ� ��ź
                                                // gunData.magCapacity : �� źâ�� �� �� �ִ� �Ѿ��� ��
        // 2. �� ���� �ʱ�ȭ(Ready)
        state = State.Ready;

        // 3. �߻� ��� �ʱ�ȭ
        lastFireTime = 0;
    }

    public void OnFire(InputValue value)
    {
        // �߻� �Լ�

        // ���� źâ�� ����ִٸ� Reload ����
        if (state == State.Empty)
            Reload();

        // ���� ���°� �غ����(Ready) �̸� ���� �߻� �������� ����� ������ �߻� ����
        if (state == State.Ready &&                             // ���� �߻� �غ� �����̸�,
            Time.time >= lastFireTime + gunData.timeBetFire)    // ���� �������� ����� ���� ���¶��(timeBetFire : �߻� ����)
        {
            animator.SetTrigger("Fire");    // �߻� Ʈ���� ���
            Shoot();                        // ���� �߻�
            lastFireTime = Time.time;       // ������ �߻� ���� ���(������ �߻� �������κ��� timeBetFire��ŭ ������ �߻� ����)
        }
    }

    public void Shoot()
    {
        // ���� ���ο��� �߻�
        if (player.equipWeaponIndex == -1 ||    // �÷��̾ �ƹ��͵� ������� �ʾҰų�(-1),
            state == State.Empty)               // ���� ����ִ� ����(Empty)���
            return;                             // ���� ������ �������� �ʰ� ����(return)

        // Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red, 0.5f);
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, fireDistance, shootableLayer, QueryTriggerInteraction.Collide))
        {
            // muzzlePoint����, muzzlePoint �� ��������, ��Ÿ�(fireDistance) ��ŭ ���Ϳ��� ���̸� ��ڴ�
            Debug.Log(hit.collider.gameObject.name);
            // ���� Ÿ���� IDamagable �������̽��� ������ �ִٸ�
            IDamagable target = hit.collider.gameObject.GetComponent<IDamagable>();

            // Ÿ�ٿ��� ���� ������(damage)��ŭ Ÿ���� ���ϰ�,
            target?.TakeHit(damage);
            
            if(((1 << hit.collider.gameObject.layer) & monsterLayer) != 0)
            {
                // Ÿ�ٿ��� �� ������ ����Ʈ �߻�
                ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                effect.transform.parent = hit.transform;
            }
        }
        // ���� �ʴ���, �ѱ����� ȭ������ �׻� ������
        muzzleFlash.Play();
        
        // Ʈ���� ������ Ȱ��ȭ
        Use();

        // �� �� �߻��� �� ���� ���� ������� ź�� ���̱�
        magCapacity--;

        if (magCapacity <= 0)       // ���� ���� ��ź�� 0�̸� 
        {
            state = State.Empty;    // ���� ��� �ִ� ����(Empty)�� ��ȯ
        }

    }

    public void Use()
    {
        // Ʈ���� ������ Ȱ��ȭ
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        // Ʈ������ �׸� �Ѿ��� �����ϰ�
        GameObject instantBullet = Instantiate(bulletObject, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        // �ش� �Ѿ˿��� �� �������� �����ֱ�
        bulletRigid.velocity = bulletPos.forward * 50;

        // 2�� �ڿ� �ش� ������Ʈ ����
        yield return new WaitForSeconds(2f);
        Destroy(instantBullet);
    }

    private void OnReload(InputValue value)
    {
        // ������
        Reload();
    }

    private void Reload()
    {
        if (state == State.Reloading ||         // ������ �����̸�,
            ammoRemain <= 0 ||                  // �����ִ� ��ź�� 0�̰�,
            magCapacity >= gunData.magCapacity) // ���� ��ź�� �ִ��� ��
        {
            return;                             // �������� �������� ����.
        }

        // ������ ����(�ڷ�ƾ, �ִϸ��̼�)
        StartCoroutine(ReloadRoutine());
        animator.SetTrigger("Reload");
    }

    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;               // ������ ���·� ��ȯ

        // ������ �Ҹ� ���
            
        yield return new WaitForSeconds(gunData.reloadTime);        //������ �ϴ� ó�� ����

        // Debug.Log($"���� ź�෮ : {magCapacity}, ���� �ܿ��� : {gunData.ammoRemain}");
        int ammoToFill = gunData.magCapacity - magCapacity;
        // Debug.Log($"ä���� �� �� : {ammoToFill}");

        if (ammoRemain < ammoToFill)                 // ��밡���� Max źâ < �ʿ��ؼ� ����� źâ
        {
            ammoToFill = ammoRemain;
        }

        magCapacity += ammoToFill;

        ammoRemain -= ammoToFill;

        state = State.Ready;
    }

}
