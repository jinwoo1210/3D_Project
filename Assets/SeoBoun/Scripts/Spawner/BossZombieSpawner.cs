using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieSpawner : ZombieSpanwer
{
    [SerializeField] Zombies fastBoss;
    [SerializeField] Zombies endureBoss;
    [SerializeField] Zombies strongBoss;

    public override void Create(ZombieClass type)
    {
        this.rank = ZombieType.boss;
        this.type = type;
        switch (type)
        {
            case ZombieClass.fast:
                CreatePool(fastBoss.prefab, 16, 16);
                curData = fastBoss.data;
                break;
            case ZombieClass.endure:
                CreatePool(endureBoss.prefab, 16, 16);
                curData = endureBoss.data;
                break;
            case ZombieClass.strong:
                CreatePool(strongBoss.prefab, 16, 16);
                curData = strongBoss.data;
                break;
        }
    }

}
