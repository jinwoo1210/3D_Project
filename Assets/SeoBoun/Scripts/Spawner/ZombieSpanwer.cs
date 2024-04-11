using System;
using System.Collections;
using UnityEngine;

public class ZombieSpanwer : ObjectPool
{
    SpawnPointActivator spawnPointActivator;
    [SerializeField] float spawnRate;       // 스폰 시간, 간격

    // TODO..
    // 좀비 데이터 추가하고, 프리팹 스폰 시 해당 데이터 넘겨주기 -> 레벨별 조절
    [SerializeField] Zombies zombieData;

    Coroutine spawnRoutine;
    bool isSpawn = false;

    public void CreatePool()
    {
        base.prefab = zombieData.prefab;
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