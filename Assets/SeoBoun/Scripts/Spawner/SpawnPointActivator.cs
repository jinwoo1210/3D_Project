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

    // -> 일정 초 마다 스폰 시작
    // -> 5초에 한번 스폰하기
    // 다만 플레이어 근처이되, 플레이어의 시야각에는 보이지 않는 곳에서 스폰시키기?
    // 

    private void Start()
    {
        for (int i = 0; i < spawnPointer.Count; i++)
        {
            spawnPointer[i].CreatePool();
            spawnPointer[i].gameObject.SetActive(true);
            spawnPointer[i].SetActivator(this);
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
