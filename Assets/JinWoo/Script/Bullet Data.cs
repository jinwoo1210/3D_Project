using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Scriptable Object/BulletData")]
public class BulletData : ScriptableObject
{
    //public enum BulletType { }
    [Header("# Main Info")]
    //public BulletType bulletType;
    public int bulletId;       //총알의 id ()
    public string bulletName;  //총알 이름 (9탄 5탄 샷건탄 등등)
    public string bulletDesc;  // 총알 설명 ( 필요하면 넣고 필요X시 삭제)
    public int bulletCount;     // 총알 주웠을때 개수
    public int magCapacity;         //현재 탄창에 남아있는 탄수
    public int ammoRemain;          //남은 전체 탄수
    public Sprite icon;     // 인벤 UI 만들때 쓰일 아이콘
    
    [Header("# Weapon")]
    public GameObject projectile;   //총알의 보여질 프리팹
}
