using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpanwer : ObjectPool
{
    SpawnPointActivator spawnPointActivator;
    [SerializeField] Monster prefab;        // 좀비 프리팹
    [SerializeField] float spawnRate;       // 스폰 시간? 간격?
    [SerializeField] int spawnCount = 1;    // 스폰 수(defalut : 1)

    Coroutine spawnRoutine;

    private void Start()
    {
        CreatePool(prefab, 10, 10);
    }

    private void OnEnable()
    {
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        spawnPointActivator.StartRespawnRoutine(this);
        StopCoroutine(spawnRoutine);
    }

    public void SetActivator(SpawnPointActivator spawnPointActivator)
    {
        this.spawnPointActivator = spawnPointActivator;
    }

    IEnumerator SpawnRoutine()
    {
        // 최대 좀비 수만큼만 하도록 수정?
        yield return new WaitForSeconds(Random.Range(1.5f, 5f));
        while (true)
        {
            PooledObject monster = GetPool(transform.position, Quaternion.identity);
            monster.transform.position += new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(spawnRoutine);
    }
}
