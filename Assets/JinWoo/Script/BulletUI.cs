using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    [SerializeField] public int curAmmo;
    [SerializeField] public int remainAmmo;

    public Gun gun;
    public GunData data;
    
    Text text;

    public void inIt(GunData gunData)
    {
        curAmmo = gun.curAmmo;       //�Ѿ� �߻�
        remainAmmo = gun.remainAmmo;
    }

    private void Awake()
    {
        inIt(data);
        Text[] texts = GetComponentsInChildren<Text>();
        text = texts[0];
    }

    private void LateUpdate()
    {
        text.text = $"{gun.curAmmo} / {gun.remainAmmo}";
    }
}
