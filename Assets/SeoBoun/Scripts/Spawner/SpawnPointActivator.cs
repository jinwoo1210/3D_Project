using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public enum ZombieType { normal, eliet, boss, size }
public class SpawnPointActivator : MonoBehaviour
{
    [SerializeField] List<ZombieSpanwer> spawnPointer = new List<ZombieSpanwer>();

    [SerializeField] int spawnLevel = 0;
    [SerializeField] int totalCount = 0;              // 총합 수
    [SerializeField] int[] maxZombieCount;            // 최대 좀비 수
    [SerializeField] int[] curZombieCount;            // 현재 좀비 수

    [SerializeField] SpawnTable[] zombieSpawnTable;
    [SerializeField] SpawnTable curZombieSpawnTable;

    public int[] MaxZombieCount { get { return maxZombieCount; } }

    private void Start()
    {
        maxZombieCount = new int[3];
        curZombieCount = new int[3];

        for (int i = 0; i < spawnPointer.Count; i++)
        {
            spawnPointer[i].Create();
            spawnPointer[i].gameObject.SetActive(true);
            spawnPointer[i].SetActivator(this);
        }
        StartCoroutine(SpawnLevelUpRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnLevelUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            spawnLevel++;
            curZombieSpawnTable = zombieSpawnTable[spawnLevel];
        }
    }

    private void SetTable()
    {
        maxZombieCount[(int)ZombieType.normal] = curZombieSpawnTable.normalSpawnCount;
        maxZombieCount[(int)ZombieType.eliet] = curZombieSpawnTable.elietSpawnCount;
        maxZombieCount[(int)ZombieType.boss] = curZombieSpawnTable.bossSpawnCount;
    }

    public void Count(ZombieType type)
    {
        curZombieCount[(int)type]++;
        totalCount++;
    }
}

[Serializable]
public struct SpawnTable
{
    public int normalSpawnCount;
    public int elietSpawnCount;
    public int bossSpawnCount;
}
