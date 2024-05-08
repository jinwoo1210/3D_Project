using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    [SerializeField] SpawnPointActivator spawnPointActivator;
    [SerializeField] Image dieImage;

    [SerializeField] AudioClip crowClip;
    [SerializeField] AudioClip animalClip;
    [SerializeField] AudioClip shortWindClip;
    [SerializeField] AudioClip wind1Clip;
    [SerializeField] AudioClip wind2Clip;
    [SerializeField] AudioClip bgmClip;
    [SerializeField] AudioClip normalSpawnClip;
    [SerializeField] AudioClip elietSpawnClip;
    [SerializeField] AudioClip bossSpawnClip;

    public AudioClip Normal { get { return normalSpawnClip; } }
    public AudioClip Eliet { get { return elietSpawnClip; } }
    public AudioClip Boss { get { return bossSpawnClip; } }

    bool isDead;
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
        PlayerStatManager.Inventory.FieldInventory.EnterScene();
        PlayerStatManager.Inventory.FieldInventory.SetUp();
        Manager.Game.FindTarget();
        Gun gun = GameObject.FindObjectOfType<Gun>();
        gun.SetGun();
    }

    private void Start()
    {
        Manager.Sound.PlayBGM(bgmClip);
    }

    public void PlayerDie()
    {
        Debug.Log("게임 씬");

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
    /*
    IEnumerator SoundEffect()
    {
        int secound = 1;
        Manager.Sound.PlayBGM(bgmClip);

        while (true)
        {
            if(secound % 7 == 0)
            {
                Manager.Sound.PlaySFX(crowClip);
            }
            else if(secound % 10 == 0)
            {
                Manager.Sound.PlaySFX(animalClip);
            }
            else if(secound % 5 == 0)
            {
                Manager.Sound.PlaySFX(shortWindClip);
            }
            else if(secound % 20 == 0)
            {
                Manager.Sound.PlaySFX(wind2Clip);
            }

            yield return new WaitForSeconds(1f);
            secound++;
        }
    }*/
}
