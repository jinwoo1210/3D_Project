using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSpawner : ItemSpawner
{
    [SerializeField] ItemSpawner[] itemSpawner;

    public override void Spawn()
    {
        int rand = Random.Range(0, itemSpawner.Length);
        itemSpawner[rand].Spawn();
    }
}
