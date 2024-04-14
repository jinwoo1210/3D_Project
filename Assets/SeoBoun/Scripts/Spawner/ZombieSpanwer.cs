using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public abstract class ZombieSpanwer : ObjectPool
{
    [SerializeField] float spawnRate;       // 스폰 시간, 간격

    [SerializeField] protected ZombieClass type;      // 좀비 타입
    [SerializeField] protected ZombieType rank;       // 좀비 등급
    [SerializeField] protected ZombieData curData;


    Coroutine spawnRoutine;
    protected bool isSpawn = false;

    public UnityEvent bossDecount;
    public UnityEvent elietDecount;
    public UnityEvent normalDecount;

    public abstract void Create(ZombieClass type);

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

    protected virtual IEnumerator SpawnRoutine()
    {
        isSpawn = true;

        yield return new WaitForSeconds(0.5f);

        Vector3 randRotation = new Vector3(0, UnityEngine.Random.Range(-180f, 180f), 0);

        PooledObject monster = GetPool(transform.position, Quaternion.Euler(randRotation));

        if (monster != null)
        {
            monster.GetComponent<Monster>().Init(curData);
            monster.GetComponent<MonsterPooledObject>().SetType(rank);
            monster.gameObject.SetActive(true);
            monster.transform.position += new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f));
        }

        isSpawn = false;

        yield return null;
    }

    private void OnDestroy()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }

    public void DeCount(ZombieType type)
    {
        if (type == ZombieType.normal)
            normalDecount?.Invoke();
        else if (type == ZombieType.eliet)
            elietDecount?.Invoke();
        else if (type == ZombieType.boss)
            bossDecount?.Invoke();
    }
}
