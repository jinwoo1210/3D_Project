using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElietZombieSpawner : ZombieSpanwer
{
    [SerializeField] Zombies fastEliet;
    [SerializeField] Zombies endureEliet;
    [SerializeField] Zombies strongEliet;

    bool isSound = false;
    Coroutine spawnSound;

    public override void Create(ZombieClass type)
    {
        if (!isSound)
            spawnSound = StartCoroutine(SoundRoutine());

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

    IEnumerator SoundRoutine()
    {
        isSound = true;
        while (true)
        {
            Manager.Sound.PlaySFX(Manager.Scene.GetCurScene().GetComponent<GameScene>().Eliet);
            yield return new WaitForSeconds(5f);
        }
    }
}
