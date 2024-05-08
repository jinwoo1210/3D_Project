using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSpawner : MonoBehaviour
{
    // 고정 스포너
    [SerializeField] int spawnCount;
    [SerializeField] ZombieClass zombieClass;  // Type : fast, endure, strong
    [SerializeField] SpawnPointActivator activator;
    [SerializeField] Zombies[] prefabZombie;

    bool isSpawn = false;

    public void SetActivator(SpawnPointActivator activator)
    {
        this.activator = activator;
        zombieClass = activator.CurClass;
    }

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        if (isSpawn)
            return;

        isSpawn = true;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randRotation = new Vector3(0, UnityEngine.Random.Range(-180f, 180f), 0);

            Monster monster = Instantiate(prefabZombie[(int)zombieClass].prefab, transform.position, Quaternion.Euler(randRotation));

            if (monster != null)
            {
                monster.GetComponent<Monster>().Init(prefabZombie[(int)zombieClass].data);
                monster.GetComponent<MonsterPooledObject>().SetType(ZombieType.size);
                monster.gameObject.SetActive(true);
                monster.transform.position += new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f));
            }
        }
    }
}
