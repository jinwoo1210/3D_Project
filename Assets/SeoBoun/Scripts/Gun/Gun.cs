using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Base Gun 코드
    [Header("Gun Stat")]
    [SerializeField] float damage;      // 데미지
    [SerializeField] int ammoCur;       // 현재 탄창 수 
    [SerializeField] int ammoMax;       // 최대 탄창 수
    [SerializeField] int ammoRemain;    // 잔여 탄창 수
    [SerializeField] float fireRate;    // 발사 속도
    [SerializeField] float reloadTime;  // 재장전 시간
    [SerializeField] float range;       // 사거리


}
