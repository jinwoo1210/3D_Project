using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointActivator : MonoBehaviour
{
    [SerializeField] List<ZombieSpanwer> spawnPointer = new List<ZombieSpanwer>();
    [SerializeField] int maxZombieCount;            // 최대 좀비 수
    [SerializeField] int curZombieCount;            // 현재 좀비 수

    private void Start()
    {
        for (int i = 0; i < spawnPointer.Count; i++)
        {
            spawnPointer[i].CreatePool();
            spawnPointer[i].gameObject.SetActive(true);
            spawnPointer[i].SetActivator(this);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
