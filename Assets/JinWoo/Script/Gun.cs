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
    public State state { get; private set; }        // ���� ���� ����

    [SerializeField] Animator animator;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] LayerMask monsterLayer;
    [SerializeField] Player player;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] GunData gunData;
    public Transform bulletPos;
    public GameObject bulletObject;
    private Text text;

    public int damage = 25; // ���ݷ�
    public int magCapacity = 25; // ���� źâ�� �����ִ� ź��
    public int ammoRemain = 50; // ���� ��ü ź��

    //public float rate = 0.12f; // �����
    public float reloadTime = 1.8f; // �����ӵ�

    private float lastFireTime;
    public float fireDistance = 100f;  //  ��Ÿ� // �÷��̾� ���Ϳ� �����Ǿ��� ����.(���Ļ� ����)

    //private void Update()
    //{
    //    Debug.Log(rate);
    //    rate += Time.deltaTime;
    //}

    private void OnEnable()
    {
        ammoRemain = gunData.ammoRemain;

        magCapacity = gunData.magCapacity;

        state = State.Ready;

        lastFireTime = 0;
    }

    public void OnFire(InputValue value)
    {
        if(state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire) // rate -> gunData.timeBatFire
        animator.SetTrigger("Fire");
        Shoot();
    }

    public void Shoot()
    {
        if (player.equipWeaponIndex == -1 || state == State.Empty)
            return;
        Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red, 0.5f);
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, 100f, monsterLayer))
        {
            IDamagable target = hit.collider.gameObject.GetComponent<IDamagable>();

            target?.TakeHit(damage);
            Debug.Log("���� ����");

            ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform;
            muzzleFlash.Play();
            Use();
        }
        magCapacity--;

        if (magCapacity <= 0)
        {
            state = State.Empty;
        }

    }

    public void Use()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        GameObject instantBullet = Instantiate(bulletObject, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return new WaitForSeconds(2f);
        Destroy(instantBullet);
    }

    private void OnReload(InputValue value)
    {
        Reload();
    }

    private void Reload()
    {
        if (state == State.Reloading || ammoRemain <= 0 || magCapacity >= gunData.magCapacity)
        {
            return;
        }
        StartCoroutine(ReloadRoutine());
        animator.SetTrigger("Reload");
    }

    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;

        // ������ �Ҹ� ���

        yield return new WaitForSeconds(gunData.reloadTime);        //������ �ϴ� ó�� ����

        Debug.Log($"���� ź�෮ : {magCapacity}, ���� �ܿ��� : {gunData.ammoRemain}");

        int ammoToFill = gunData.magCapacity - magCapacity;
        Debug.Log($"ä���� �� �� : {ammoToFill}");

        if (ammoRemain < ammoToFill)                 // ��밡���� Max źâ < �ʿ��ؼ� ����� źâ
        {
            ammoToFill = ammoRemain;
        }

        magCapacity += ammoToFill;

        ammoRemain -= ammoToFill;

        state = State.Ready;
    }

}
