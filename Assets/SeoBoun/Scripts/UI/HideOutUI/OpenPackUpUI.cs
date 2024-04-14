using System;
using UnityEngine;

public class OpenPackUpUI : OpenUI
{
    [SerializeField] UpgradePackLevel packLevel;

    public void AddMedicalEvent()
    {
        packLevel.Upgrade += packLevel.UpgradeMedicalLevel;
    }

    public void AddFoodEvent()
    {
        packLevel.Upgrade += packLevel.UpgradeFoodLevel;
    }

    public void AddElectEvent()
    {
        packLevel.Upgrade += packLevel.UpgradeElectLevel;
    }

    public void AddToolEvent()
    {
        packLevel.Upgrade += packLevel.UpgradeToolLevel;
    }

}
