using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    //public AudioClip shotClip;
    //public AudioClip reloadClip;

    // ���� ���¸� ǥ���ϴ� Ÿ���� ����
    public enum State { Ready, Empty, Reloading}    // �߻� �غ�, źâ ��, ������ ��
    public State state { get; private set; }        // ���� ���� ����

    [SerializeField] Animator animator;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] LayerMask monsterLayer;
    [SerializeField] Player player;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem hitEffect;
    public Transform bulletPos;
    public GameObject bulletObject;
    private Text text;

    public int damage = 25; // ���ݷ�
    public int magCapacity = 25; // ���� źâ�� �����ִ� ź��
    public int ammoReMain = 50; // ���� ��ü ź��

    public float rate = 0.12f; // �����
    public float reloadTime = 1.8f; // �����ӵ�

    public float fireDistance = 100f;  //  ��Ÿ� // �÷��̾� ���Ϳ� �����Ǿ��� ����.(���Ļ� ����)

    public void OnFire(InputValue value)
    {
        Debug.Log("Onfire �۵�");
        animator.SetTrigger("Fire");
        Shoot();
        player.Attack();
    }

    private void Awake()
    {
        Shoot();
        Text[] texts = GetComponentsInChildren<Text>();
        text = texts[0];
    }

    private void LateUpdate()
    {
        text.text = magCapacity + "/" + ammoReMain;
    }

    public void Shoot()
    {
        magCapacity--;
        muzzleFlash.Play();
        Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward, Color.red, 0.5f);
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, 100f, monsterLayer))
        {
            IDamagable target = hit.collider.gameObject.GetComponent<IDamagable>();

            target?.TakeHit(damage);
            Debug.Log("���� ����");

            ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform;
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
        animator.SetTrigger("Reload");
    }
}
