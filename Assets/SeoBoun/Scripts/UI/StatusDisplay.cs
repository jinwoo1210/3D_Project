using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField] Slider hpBar;
    [SerializeField] PlayerStat stat;

    private void Start()
    {
        stat.ChangePlayerHp += ChangeHpBar;
    }

    public void ChangeHpBar(int maxHp, int curHp)
    {
        hpBar.value = (float)curHp / maxHp;
    }
}
