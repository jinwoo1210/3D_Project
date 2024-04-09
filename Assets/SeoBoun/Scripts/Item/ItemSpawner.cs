using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSpawner : MonoBehaviour
{
    // TODO... 아이템 스폰기능 만들기
    [SerializeField] protected BaseItem[] prefab;
    [SerializeField] protected int[] percentage;

    protected int rand;
    // 확률 테이블 존재 -> 확률 테이블에 따른 1회 스폰

    public abstract void Spawn();
}
