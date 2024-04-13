using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    [SerializeField] TMP_Text magAmmoText;
    [SerializeField] TMP_Text remainAmmoText;
    [SerializeField] WeaponHolder holder;

    [ContextMenu("SetUp")]
    public void SetUp()
    {
        holder.CurEquipGun.DeleteEvent();
        holder.CurEquipGun.AddMagAmmo(ShowMagAmmoText);
        holder.CurEquipGun.AddAmmoRemain(ShowRemainAmmoText);
    }

    private void Start()
    {
        SetUp();
    }

    public void ShowMagAmmoText(int magAmmo)
    {
        magAmmoText.text = magAmmo.ToString();
    }

    public void ShowRemainAmmoText(int remainAmmo)
    {
        remainAmmoText.text = remainAmmo.ToString();
    }
}
