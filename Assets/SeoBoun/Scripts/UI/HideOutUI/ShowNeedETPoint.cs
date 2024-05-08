using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowNeedETPoint : MonoBehaviour
{
    // Ω∫≈»¿∫ Medical, Food
    // πÈ∆—¿∫ Elect, Tool
    // √—µµ Elect, Tool

    [SerializeField] TMP_Text electPoint;
    [SerializeField] TMP_Text toolPoint;

    public void ShowMedicalPoint()
    {
        int medicalLevel = PlayerStatManager.Inventory.medicalLevel;

        if (medicalLevel == 3)
            return;

        electPoint.text = (PlayerStatManager.Inventory.packLevelUpPoint[medicalLevel].electPoint).ToString();
        toolPoint.text = (PlayerStatManager.Inventory.packLevelUpPoint[medicalLevel].toolPoint).ToString();
    }

    public void ShowFoodPoint()
    {
        int foodLevel = PlayerStatManager.Inventory.foodLevel;

        if (foodLevel == 3)
            return;

        electPoint.text = (PlayerStatManager.Inventory.packLevelUpPoint[foodLevel].electPoint).ToString();
        toolPoint.text = (PlayerStatManager.Inventory.packLevelUpPoint[foodLevel].toolPoint).ToString();
    }

    public void ShowElectPoint()
    {
        int electLevel = PlayerStatManager.Inventory.electLevel;

        if (electLevel == 3)
            return;

        electPoint.text = (PlayerStatManager.Inventory.packLevelUpPoint[electLevel].electPoint).ToString();
        toolPoint.text = (PlayerStatManager.Inventory.packLevelUpPoint[electLevel].toolPoint).ToString();
    }

    public void ShowToolPoint()
    {
        int toolLevel = PlayerStatManager.Inventory.toolLevel;

        if (toolLevel == 3)
            return;
 
        electPoint.text = (PlayerStatManager.Inventory.packLevelUpPoint[toolLevel].electPoint).ToString();
        toolPoint.text = (PlayerStatManager.Inventory.packLevelUpPoint[toolLevel].toolPoint).ToString();
    }

    public void ShowSMGPoint()
    {
        int SMGTotal = PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SMG].totalLevel;

        if (SMGTotal == 15)
            return;
        electPoint.text = (PlayerStatManager.Inventory.gunLevelUpPoint[SMGTotal / 3].electPoint).ToString();
        toolPoint.text = (PlayerStatManager.Inventory.gunLevelUpPoint[SMGTotal / 3].toolPoint).ToString();
    }

    public void ShowARPoint()
    {
        int ARTotal = PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.AR].totalLevel;

        if (ARTotal == 15)
            return;
        electPoint.text = (PlayerStatManager.Inventory.gunLevelUpPoint[ARTotal / 3].electPoint).ToString();
        toolPoint.text = (PlayerStatManager.Inventory.gunLevelUpPoint[ARTotal / 3].toolPoint).ToString();
    }
    public void ShowSGPoint()
    {
        int SGTotal = PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SG].totalLevel;

        if (SGTotal == 15)
            return;
        electPoint.text = (PlayerStatManager.Inventory.gunLevelUpPoint[SGTotal / 3].electPoint).ToString();
        toolPoint.text = (PlayerStatManager.Inventory.gunLevelUpPoint[SGTotal / 3].toolPoint).ToString();
    }
    public void ShowSRPoint()
    {
        int SRTotal = PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SR].totalLevel;

        if (SRTotal == 15)
            return;
        electPoint.text = (PlayerStatManager.Inventory.gunLevelUpPoint[SRTotal / 3].electPoint).ToString();
        toolPoint.text = (PlayerStatManager.Inventory.gunLevelUpPoint[SRTotal / 3].toolPoint).ToString();
    }

}
