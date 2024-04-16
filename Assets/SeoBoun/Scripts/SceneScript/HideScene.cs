using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideScene : BaseScene
{
    [SerializeField] StatusInfoUI statusInfoUI;

    public int Count = 0;

    [SerializeField] AudioClip bgmClip;

    public override IEnumerator LoadingRoutine()
    {
        PlayerStatManager.Inventory.FieldInventory.ExitScene();
        yield return null;
    }

    private void Start()
    {
        if (bgmClip != null)
            Manager.Sound.PlayBGM(bgmClip);
    }

    public void UpdateInfo()
    {
        if(statusInfoUI == null)
        {
            statusInfoUI = FindObjectOfType<StatusInfoUI>();
        }

        PlayerStatManager.Inventory.playerStat.LevelUp();
        statusInfoUI.ShowPackInfo();
        statusInfoUI.ShowStatusInfo();
        switch (Count)
        {

            case 0:
                statusInfoUI.ShowGunInfo(WeaponType.SMG);
                break;
            case 1:
                statusInfoUI.ShowGunInfo(WeaponType.AR);
                break;
            case 2:
                statusInfoUI.ShowGunInfo(WeaponType.SG);
                break;
            case 3:
                statusInfoUI.ShowGunInfo(WeaponType.SR);
                break;
        }
    }

    public void ExitScene()
    {
        Manager.Scene.LoadScene("GameScene");
    }

}
