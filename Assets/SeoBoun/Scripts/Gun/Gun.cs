using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Base Gun �ڵ�
    [Header("Gun Stat")]
    [SerializeField] float damage;      // ������
    [SerializeField] int ammoCur;       // ���� źâ �� 
    [SerializeField] int ammoMax;       // �ִ� źâ ��
    [SerializeField] int ammoRemain;    // �ܿ� źâ ��
    [SerializeField] float fireRate;    // �߻� �ӵ�
    [SerializeField] float reloadTime;  // ������ �ð�
    [SerializeField] float range;       // ��Ÿ�


}
