using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombieSpawner : ZombieSpanwer
{
    [SerializeField] Zombies fastNormal;
    [SerializeField] Zombies endureNormal;
    [SerializeField] Zombies strongNormal;

    public override void Create(ZombieClass type)
    {
        this.rank = ZombieType.normal;
        this.type = type;
        switch (type)
        {
            case ZombieClass.fast:
                CreatePool(fastNormal.prefab, 16, 16);
                curData = fastNormal.data;
                break;
            case ZombieClass.endure:
                CreatePool(endureNormal.prefab, 16, 16);
                curData = endureNormal.data;
                break;
            case ZombieClass.strong:
                CreatePool(strongNormal.prefab, 16, 16);
                curData = strongNormal.data;
                break;
        }
    }
}
