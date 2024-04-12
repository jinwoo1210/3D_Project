using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElietZombieSpawner : ZombieSpanwer
{
    [SerializeField] Zombies fastEliet;
    [SerializeField] Zombies endureEliet;
    [SerializeField] Zombies strongEliet;

    public override void Create(ZombieClass type)
    {
        this.rank = ZombieType.eliet;
        this.type = type;
        switch (type)
        {
            case ZombieClass.fast:
                CreatePool(fastEliet.prefab, 16, 16);
                curData = fastEliet.data;
                break;
            case ZombieClass.endure:
                CreatePool(endureEliet.prefab, 16, 16);
                curData = endureEliet.data;
                break;
            case ZombieClass.strong:
                CreatePool(strongEliet.prefab, 16, 16);
                curData = strongEliet.data;
                break;
        }
    }
}
