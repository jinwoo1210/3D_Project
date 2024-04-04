using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] public int magCapacity;
    [SerializeField] public int ammoReMain;
    [SerializeField] public int bulletCount;
    public BulletData data;
    
    Text text;

    public void inIt(BulletData bulletData)
    {
        magCapacity = bulletData.magCapacity;       //ÃÑ¾Ë ¹ß»ç
        ammoReMain = bulletData.ammoRemain;
        bulletCount = bulletData.bulletCount;

    }

    private void Awake()
    {
        inIt(data);
        Text[] texts = GetComponentsInChildren<Text>();
        text = texts[0];
    }

    private void LateUpdate()
    {
        text.text = magCapacity + "/" + ammoReMain;
    }
}
