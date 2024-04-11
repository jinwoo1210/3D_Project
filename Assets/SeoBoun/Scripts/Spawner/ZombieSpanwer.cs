using System;
using System.Collections;
using UnityEngine;

public class ZombieSpanwer : ObjectPool
{
    SpawnPointActivator spawnPointActivator;
    [SerializeField] float spawnRate;       // ���� �ð�, ����

    // TODO..
    // ���� ������ �߰��ϰ�, ������ ���� �� �ش� ������ �Ѱ��ֱ� -> ������ ����
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
        // �ִ� ���� ����ŭ�� �ϵ��� ���� -> ���� ���� �� 10���� �ִ� ����
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