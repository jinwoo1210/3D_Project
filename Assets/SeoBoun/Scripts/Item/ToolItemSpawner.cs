using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolItemSpawner : ItemSpawner
{
    private void Start()
    {
        rand = Random.Range(0, 10) + 1;

        if (rand > 7)
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
