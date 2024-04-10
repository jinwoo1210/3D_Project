using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectItemSpawner : ItemSpawner
{
    private void Start()
    {
        Spawn();
    }

    public override void Spawn()
    {
        rand = Random.Range(0, 1000) + 1;
        int count = 0;

        for (int i = 0; i < prefab.Length; i++)
        {
            count += percentage[i];
            if (rand <= count)
            {
                Instantiate(prefab[i], transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
