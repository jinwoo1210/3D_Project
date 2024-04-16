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
            Debug.Log("½ºÆù¤¼µõ");
        }
    }

    public override void Spawn()
    {
        int rand = Random.Range(0, itemSpawner.Length);
        itemSpawner[rand].Spawn();
    }
}
