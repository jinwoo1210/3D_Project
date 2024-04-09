using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpanwer : ObjectPool
{
    SpawnPointActivator spawnPointActivator;
    [SerializeField] float spawnRate;       // ���� �ð�, ����

    // TODO..
    // ���� ������ �߰��ϰ�, ������ ���� �� �ش� ������ �Ѱ��ֱ� -> ������ ����
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
        // �ִ� ���� ����ŭ�� �ϵ��� ���� -> ���� ���� �� 10���� �ִ� ����
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
