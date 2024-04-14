using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStatUpUI : OpenUI
{
    // ���ȿ� ���� �̺�Ʈ ���
    [SerializeField] UpgradeStatLevel statLevel;

    public void AddHpEvent()
    {
        statLevel.Upgrade += statLevel.UpgradeHpLevel;
    }

    public void AddStaminaEvent()
    {
        statLevel.Upgrade += statLevel.UpgradeStaminaLevel;
    }

    public void AddSpeedEvent()
    {
        statLevel.Upgrade += statLevel.UpgradeSpeedLevel;
    }
}
