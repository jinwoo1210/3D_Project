using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] public int magCapacity;
    [SerializeField] public int ammoRemain;
    [SerializeField] public int bulletCount;
    public GunData data;
    public Gun gun;
    
    Text text;

    public void inIt(GunData gunData)
    {
        magCapacity = gunData.magCapacity;       //ÃÑ¾Ë ¹ß»ç
        ammoRemain = gunData.ammoRemain;
        //bulletCount = gunData.bulletCount;

    }

    private void Awake()
    {
        inIt(data);
        Text[] texts = GetComponentsInChildren<Text>();
        text = texts[0];
    }

    private void LateUpdate()
    {
        text.text = gun.magCapacity + "/" + gun.ammoRemain;
    }
}
