using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ZombieSpanwer : ObjectPool
{
    SpawnPointActivator spawnPointActivator;
    [SerializeField] float spawnRate;       // 스폰 시간, 간격

    // TODO..
    // 좀비 데이터 추가하고, 프리팹 스폰 시 해당 데이터 넘겨주기 -> 레벨별 조절
    [SerializeField] Zombies zombieData;
    [SerializeField] ZombieData[] zombies;
    [SerializeField] Monster[] prefabs;

    Coroutine spawnRoutine;
    bool isSpawn = false;

    public void Create()
    {
        
    }

    public override void CreatePool(PooledObject prefab, int size, int capacity)
    {
        this.prefab = prefab;
        this.size = size;
        this.capacity = capacity;

        objectPool = new Stack<PooledObject>(capacity);
        for (int i = 0; i < size; i++)
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.Pool = this;
            instance.transform.parent = transform;
            objectPool.Push(instance);
        }
    }

    public void StartSpawn()
    {
        if (!isSpawn)
            spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawn()
    {
        StopCoroutine(spawnRoutine);
    }

    public void SetActivator(SpawnPointActivator spawnPointActivator)
    {
        this.spawnPointActivator = spawnPointActivator;
    }

    IEnumerator SpawnRoutine()
    {
        isSpawn = true;

        yield return new WaitForSeconds(0.5f);

        PooledObject monster = GetPool(transform.position, Quaternion.identity);

        if (monster != null)
        {
            monster.GetComponent<Monster>().Init(zombieData.normalZombieData);
            monster.gameObject.SetActive(true);
            monster.transform.position += new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f));
        }

        isSpawn = false;
    }

    private void OnDestroy()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }
    
}

[Serializable]
public struct Zombies
{
    public ZombieData normalZombieData;
    public ZombieData eliteZombieData;
    public ZombieData bossZombieData;

    public PooledObject prefab;
}