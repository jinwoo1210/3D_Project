using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] int magCapacity;
    public BulletData data;
    
    Text text;

    public void inIt(BulletData bulletData)
    {
        magCapacity = bulletData.MagCapacity;
    }

    private void Awake()
    {
        inIt(data);
        Text[] texts = GetComponentsInChildren<Text>();
        text = texts[0];
    }

    private void LateUpdate()
    {
        text.text = data.MagCapacity + "/" + data.AmmoRemain;
    }
}
