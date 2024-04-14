using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGunUpUI : OpenUI
{
    [SerializeField] UpgradeGunLevel gunLevel;

    public void AddSMGEvent()
    {
        gunLevel.Upgrade += gunLevel.UpgradeSMGLevel;
    }

    public void AddAREvent()
    {
        gunLevel.Upgrade += gunLevel.UpgradeARLevel;
    }

    public void AddSGEvent()
    {
        gunLevel.Upgrade += gunLevel.UpgradeSGLevel;
    }

    public void AddSREvent()
    {
        gunLevel.Upgrade += gunLevel.UpgradeSRLevel;
    }

}
