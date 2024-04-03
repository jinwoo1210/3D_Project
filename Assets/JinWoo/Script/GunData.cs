using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Object/GunData")]
public class GunData : ScriptableObject
{
    //public enum GunType { }
    [Header("# Main Info")]
    //public GunType gunType;
    public int gunId;       //무기의 id (총불러올때 쓰임)
    public string gunName;  //무기 이름 (SMG,Vecter,Sniper 등등)
    public string gunDesc;  // 무기 설명 ( 필요하면 넣고 필요X시 삭제)
    public Sprite icon;     // 인벤 UI 만들때 쓰일 아이콘

    [Header("# Level Data")]
    public float BaseDamage;        //무기의 기본 데미지
    // 무기강화시 추가적으로 레벨 수치 입력예정 ( 데미지, 연사력)
    public float[] Damage;      //데미지레벨별 증가률
    public float[] ShootSpeed;  //연사력레벨별 증가률
    [Header("# Weapon")]
    public GameObject projectile;   //무기의 보여질 프리팹
}
