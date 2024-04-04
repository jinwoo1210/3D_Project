using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField] Slider hpBar;
    [SerializeField] Slider staminaBar;
    [SerializeField] PlayerStat stat;

    private void Awake()
    {
        stat.ChangePlayerHp += ChangeHpBar;
        stat.ChangePlayerStamina += ChangeStamina;
    }

    public void ChangeHpBar(int maxHp, int curHp)
    {
        hpBar.value = (float)curHp / maxHp;
    }

    public void ChangeStamina(int maxStamina, int curStamina)
    {
        staminaBar.value = (float)curStamina / maxStamina;
    }
}
