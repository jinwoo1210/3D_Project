using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpanwer : MonoBehaviour
{
    SpawnPointActivator spawnPointActivator;
    [SerializeField] Monster prefab;        // ���� ������
    [SerializeField] float spawnRate;       // ���� �ð�? ����?
    [SerializeField] int spawnCount = 1;    // ���� ��(defalut : 1)

    Coroutine spawnRoutine;

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
        // �ִ� ���� ����ŭ�� �ϵ��� ����?
        yield return new WaitForSeconds(Random.Range(1.5f, 5f));
        while (true)
        {
            Monster monster = Instantiate(prefab, transform.position, Quaternion.identity);
            monster.transform.position += new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(spawnRoutine);
    }
}
