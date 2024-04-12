using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public enum ZombieType { normal, eliet, boss, size }
public enum ZombieClass { fast, endure, strong, size}
public enum SpawnState { normal, eliet, boss, noSpawn, size}
public class SpawnPointActivator : MonoBehaviour
{
    [SerializeField] List<SpawnManagement> spawnPointer = new List<SpawnManagement>();

    [SerializeField] int spawnLevel = 0;
    [SerializeField] int totalCount = 0;              // 총합 수
    [SerializeField] int[] maxZombieCount;            // 최대 좀비 수
    [SerializeField] int[] curZombieCount;            // 현재 좀비 수

    [SerializeField] SpawnTable[] zombieSpawnTable;
    [SerializeField] SpawnTable curZombieSpawnTable;

    [SerializeField] ZombieClass curClass;

    public int[] MaxZombieCount { get { return maxZombieCount; } }
    public int SpawnLevel { get { return spawnLevel; } }

    [ContextMenu("Create")]
    public void StartGame()
    {
        maxZombieCount = new int[3];
        curZombieCount = new int[3];

        for (int i = 0; i < spawnPointer.Count; i++)
        {
            spawnPointer[i].SetActivator(this);
            spawnPointer[i].SetSpawn(curClass);
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
            SetTable();
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

    public void BossDeCount()
    {
        curZombieCount[(int)ZombieType.boss]--;
        totalCount--;
    }
    public void ElietDeCount()
    {
        curZombieCount[(int)ZombieType.eliet]--;
        totalCount--;
    }
    public void NormalDeCount()
    {
        curZombieCount[(int)ZombieType.normal]--;
        totalCount--;
    }

    public void SetZombieClass()
    {
        curClass = (ZombieClass)UnityEngine.Random.Range(0, (int)ZombieClass.size) ;
        Debug.Log($"이번 맵의 좀비 타입 : {curClass}");
    }

    public SpawnState canSpawn()
    {
        int normalCount = curZombieCount[(int)ZombieType.normal];
        int elietCount = curZombieCount[(int)ZombieType.eliet];
        int bossCount = curZombieCount[(int)ZombieType.boss];

        int maxNormal = maxZombieCount[(int)ZombieType.normal];
        int maxEliet = maxZombieCount[(int)ZombieType.eliet];
        int maxBoss = maxZombieCount[(int)ZombieType.boss];

        Debug.Log($"좀비의 총 수 : {totalCount}");
        Debug.Log($"normal : {normalCount}, eliet : {elietCount}, boss : {bossCount}");
        Debug.Log($"스폰 레벨 : {spawnLevel}, (maxNormal : {maxNormal}, maxEliet : {maxEliet}, maxBoss : {maxBoss}");

        if(totalCount >= maxNormal + maxEliet + maxBoss)
        {   // 좀비의 총 수가 벗어났다면, 스폰 정지
            return SpawnState.noSpawn;
        }

        if (normalCount < maxNormal)
            return SpawnState.normal;

        if (elietCount < maxEliet)
            return SpawnState.eliet;

        return SpawnState.boss;

    }
}

[Serializable]
public struct SpawnTable
{
    public int normalSpawnCount;
    public int elietSpawnCount;
    public int bossSpawnCount;
}
