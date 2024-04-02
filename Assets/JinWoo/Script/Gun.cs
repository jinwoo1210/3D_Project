using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25; // 공격력
    public int magCapacity = 25; // 현재 탄창에 남아있는 탄수
    public int ammoReMain = 50; // 남은 전체 탄수

    public float timeBetFire = 0.12f; // 연사력
    public float reloadTime = 1.8f; // 장전속도

    public float fireDistance = 100f;  //  사거리 // 플레이어 슈터에 구현되어져 있음.(형식상 구현)
}
