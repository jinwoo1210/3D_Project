using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] SpawnPointActivator spawnPointActivator;
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
        if (Manager.Game.playerPos == null)
        {
            Manager.Game.playerPos = GameObject.FindWithTag("Player").transform;
        }
        // 좀비 클래스 설정 및 오브젝트 풀 활성화
        spawnPointActivator.SetZombieClass();
        spawnPointActivator.StartGame();
        PlayerStatManager.Inventory.playerStat.SetUp();
        Manager.Game.FindTarget();
    }

}
