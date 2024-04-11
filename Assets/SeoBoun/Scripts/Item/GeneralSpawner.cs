using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSpawner : ItemSpawner
{
    [SerializeField] ItemSpawner[] itemSpawner;

    private void Start()
    {
        Spawn();
    }

    public override void Spawn()
    {
        int rand = Random.Range(0, itemSpawner.Length);
        itemSpawner[rand].Spawn();
    }
}
