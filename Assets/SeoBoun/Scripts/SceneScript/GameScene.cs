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
        // ���� Ŭ���� ���� �� ������Ʈ Ǯ Ȱ��ȭ
        spawnPointActivator.SetZombieClass();
        spawnPointActivator.StartGame();
        PlayerStatManager.Inventory.playerStat.SetUp();
        Manager.Game.FindTarget();
    }

}
