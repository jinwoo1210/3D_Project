using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] int magCapacity;
    [SerializeField] public int ammoReMain;
    [SerializeField] public int bulletCount;
    public BulletData data;
    
    Text text;

    public void inIt(BulletData bulletData)
    {
        magCapacity = bulletData.MagCapacity;       //ÃÑ¾Ë ¹ß»ç
        ammoReMain = bulletData.AmmoRemain;
        bulletCount = bulletData.bulletCount;

    }

    private void Awake()
    {
        inIt(data);
        Text[] texts = GetComponentsInChildren<Text>();
        if(texts != null )
        text = texts[0];
    }

    private void LateUpdate()
    {
        if(text != null)
        text.text = data.MagCapacity + "/" + data.AmmoRemain;
    }
}
