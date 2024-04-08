using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpanwer : ObjectPool
{
    SpawnPointActivator spawnPointActivator;
    [SerializeField] float spawnRate;       // 스폰 시간, 간격

    // TODO..
    // 좀비 데이터 추가하고, 프리팹 스폰 시 해당 데이터 넘겨주기
    [SerializeField] List<ZombieData> spawnData;

    Coroutine spawnRoutine;
    bool isSpawn = false;

    public void CreatePool()
    {
        base.CreatePool(prefab, 7, 7);
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
        // 최대 좀비 수만큼만 하도록 수정 -> 스폰 지점 별 10마리 최댓값 설정
        isSpawn = true;

        yield return new WaitForSeconds(1f);

        PooledObject monster = GetPool(transform.position, Quaternion.identity);

        if (monster != null)
        {
            monster.GetComponent<Monster>().Init(spawnData[Random.Range(0, spawnData.Count)]);
            monster.gameObject.SetActive(true);
            monster.transform.position += new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        }

        isSpawn = false;
    }

    private void OnDestroy()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }

}
