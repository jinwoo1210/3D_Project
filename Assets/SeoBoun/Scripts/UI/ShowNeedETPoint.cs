using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowNeedETPoint : MonoBehaviour
{
    // Ω∫≈»¿∫ Medical, Food
    // πÈ∆—¿∫ Elect, Tool

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
}
