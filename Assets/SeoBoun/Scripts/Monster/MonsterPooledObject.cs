using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPooledObject : PooledObject
{
    [SerializeField] ZombieType curType;

    public void SetType(ZombieType type)
    {
        this.curType = type;
    }

    public void Decount(ZombieType type)
    {
        if (type == ZombieType.size)
            return;

        this.Pool.GetComponent<ZombieSpanwer>()?.DeCount(type);
    }

    public override void SetAutoRelease()
    {
        Decount(this.curType);
        StartCoroutine(ReleaseRoutine());
    }
}
