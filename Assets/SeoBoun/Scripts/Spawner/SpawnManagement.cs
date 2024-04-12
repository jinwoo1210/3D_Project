using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManagement : MonoBehaviour
{
    [SerializeField] ZombieSpanwer normalSpawner;
    [SerializeField] ZombieSpanwer elietSpawner;
    [SerializeField] ZombieSpanwer bossSpawner;

    [SerializeField] SpawnPointActivator activator;

    public void SetSpawn(ZombieClass type)
    {
        normalSpawner.Create(type);
        elietSpawner.Create(type);
        bossSpawner.Create(type);
    }

    public void SetActivator(SpawnPointActivator activator)
    {
        this.activator = activator;
    }

    public void Spawn()
    {
        SpawnState curState = activator.canSpawn();

        Debug.Log($"현재 스폰상태 : {curState}");

        switch(curState)
        {
            case SpawnState.normal:
                normalSpawner.StartSpawn();
                activator.Count(ZombieType.normal);
                break;
            case SpawnState.eliet:
                elietSpawner.StartSpawn();
                activator.Count(ZombieType.eliet);
                break;
            case SpawnState.boss:
                bossSpawner.StartSpawn();
                activator.Count(ZombieType.boss);
                break;
            case SpawnState.noSpawn:
                break;
        }
    }
}
