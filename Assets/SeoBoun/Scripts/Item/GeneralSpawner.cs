using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSpawner : ItemSpawner
{
    [SerializeField] ItemSpawner[] itemSpawner;

    private void Start()
    {
        rand = Random.Range(0, 10) + 1;

        if (rand > 7)
        {
            Spawn();
        }
    }

    public override BaseItem Spawn()
    {
        int rand = Random.Range(0, itemSpawner.Length);
        BaseItem instance = itemSpawner[rand].Spawn();

        instance.transform.position = transform.position;

        return null;
    }
}
