using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointActivator : MonoBehaviour
{
    [SerializeField] List<ZombieSpanwer> spawnPointer = new List<ZombieSpanwer>();
    [SerializeField] int activateSpawnerCount;      // Ȱ��ȭ �� ���� �������� ��
    [SerializeField] int maxZombieCount;            // �ִ� ���� ��
    [SerializeField] int curZombieCount;            // ���� ���� ��

    // �����ʴ� ���� �� ��Ȱ��ȭ �Ǹ� Ư�� �ʰ� ������ Ȱ��ȭ)
    // �÷��̾ �ٶ󺸸� ��Ȱ��ȭ�� �ǰ�, �ڵ����� �� �ʵڿ� Ȱ��ȭ

    private void Start()
    {
        for (int i = 0; i < spawnPointer.Count; i++)
        {
            spawnPointer[i].CreatePool();
            spawnPointer[i].gameObject.SetActive(false);
            spawnPointer[i].SetActivator(this);
            StartRespawnRoutine(spawnPointer[i]);
        }
    }

    public void StartRespawnRoutine(ZombieSpanwer target)
    {
        if (target.gameObject.activeSelf == false)
            StartCoroutine(RespawnRoutine(target));
    }

    IEnumerator RespawnRoutine(ZombieSpanwer target)
    {
        yield return new WaitForSeconds(5f);
        target.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
