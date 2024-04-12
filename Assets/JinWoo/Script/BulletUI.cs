using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    [SerializeField] public int curAmmo;
    [SerializeField] public int remainAmmo;

    public Player player;
    public Gun[] gun;
    public GunData[] data;
    
    Text text;

    public void inIt(GunData gunData)
    {
        for (int i = 0; i < player.hasWeapon.Length; i++)
        {
            if (player.equipWeaponIndex == i)
            {
                Debug.Log($"{player.equipWeaponIndex}");
                Debug.Log($"{i}");
                curAmmo = gun[i].curAmmo;       //i¹øÂ° ÃÑÀÇ ÃÑ¾Ë ¹ß»ç -1
                remainAmmo = gun[i].remainAmmo;// i¹øÂ° ÃÑÀÇ »ç¿ëµÉ ÃÑÅºÃ¢ 
            }
        }
    }

    private void Awake()
    {
        for(int i = 0; i < data.Length; i++)
        {
            //Debug.Log(data[i].name);
            inIt(data[i]);
            Text[] texts = GetComponentsInChildren<Text>();
            text = texts[i];
        }
    }

    private void LateUpdate()
    {
        for(int i = 0; i < player.hasWeapon.Length; i++)
        text.text = $"{gun[i].curAmmo} / {gun[i].remainAmmo}";
    }
}
