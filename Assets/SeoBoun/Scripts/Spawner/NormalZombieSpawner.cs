using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombieSpawner : ZombieSpanwer
{
    [SerializeField] Zombies fastNormal;
    [SerializeField] Zombies endureNormal;
    [SerializeField] Zombies strongNormal;

    bool isSpawn = false;
    Coroutine spawnSound;

    public override void Create(ZombieClass type)
    {
        if (!isSpawn)
            spawnSound = StartCoroutine(SoundRoutine());

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
    
    IEnumerator SoundRoutine()
    {
        isSpawn = true;
        AudioClip clip = Manager.Scene.GetCurScene().GetComponent<GameScene>().Normal;
        while (true)
        {
            Manager.Sound.PlaySFX(clip);
            yield return new WaitForSeconds(5f);
        }
    }
}
