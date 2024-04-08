using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Object/GunData")]
public class GunData : ScriptableObject
{
    public enum GunType { AR, SMG, SHOTGUN, SNIPER}     // 무기의 타입
    [Header("# Main Info")]

    public GunType gunType;
    public int gunId;       //무기의 id (총불러올때 쓰임)
    public string gunName;  //무기 이름 (SMG,Vecter,Sniper 등등)
    public string gunDesc;  // 무기 설명 ( 필요하면 넣고 필요X시 삭제)

    public int curAmmo;     //실제 사용되어지는 탄창      

    public Sprite icon;     // 인벤 UI 만들때 쓰일 아이콘

    [Header("# Level Data")]
    public LevelData[] datas;

    [Header("# Weapon")]
    public GameObject projectile;   //무기의 보여질 프리팹
}

[Serializable]
public struct LevelData
{
    // 무기강화시 추가적으로 레벨 수치 입력
    public int damage;        //데미지
    public float shootSpeed;    //연사속도
    public int remainAmmo;     //탄창용량
    public float reloadTime;    //장전시간
    public float fireDistance;  //공격 사거리
}