using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GunData[] gunData;
    public Gun[] gun;
    public int level = 0;
    public Player player;
    private void Update()
    {
        LevelUp();
    }

    void LevelUp()
    {
        //현재 장착중인 총의 종류?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < gunData.Length; i++)
            {
                //gunData.SetValue(player.equipWeaponIndex, i );
                Debug.Log($"equipWeaponIndex = {player.equipWeaponIndex}");
                Debug.Log($"gunData = {gunData}");
                Debug.Log($"i = {i}");
                if ((level <= gunData[i].datas.Length))
                {
                    level++;
                    gun[i].Setup(level);
                    Debug.Log(level);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
