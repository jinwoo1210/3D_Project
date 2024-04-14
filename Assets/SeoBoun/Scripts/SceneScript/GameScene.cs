using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    [SerializeField] SpawnPointActivator spawnPointActivator;
    [SerializeField] Image dieImage;

    bool isDead;
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
        Gun gun = GameObject.FindObjectOfType<Gun>();
        gun.SetGun();
    }

    public void PlayerDie()
    {
        Debug.Log("���� ��");

        if (!isDead)
            StartCoroutine(SceneLoad());

        isDead = true;
        dieImage.gameObject.SetActive(true);
        PlayerStatManager.Inventory.FieldInventory.ExitScene_PlayerDie();
    }

    IEnumerator SceneLoad()
    {
        yield return new WaitForSeconds(3f);

        Manager.Scene.LoadScene("HideScene");
    }
}
