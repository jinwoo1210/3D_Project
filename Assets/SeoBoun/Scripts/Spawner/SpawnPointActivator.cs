using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointActivator : MonoBehaviour
{
    [SerializeField] List<ZombieSpanwer> spawnPointer = new List<ZombieSpanwer>();
    [SerializeField] int activateSpawnerCount;      // 활성화 된 좀비 스포너의 수
    [SerializeField] int maxZombieCount;            // 최대 좀비 수
    [SerializeField] int curZombieCount;            // 현재 좀비 수

    // 스포너는 시작 시 비활성화 되며 특정 초가 지나면 활성화)
    // 플레이어가 바라보면 비활성화가 되고, 자동으로 수 초뒤에 활성화

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
