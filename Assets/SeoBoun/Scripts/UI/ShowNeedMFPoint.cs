using TMPro;
using UnityEngine;

public class ShowNeedMFPoint : MonoBehaviour
{
    // Ω∫≈»¿∫ Medical, Food
    // πÈ∆—¿∫ Elect, Tool
    [SerializeField] TMP_Text medicalPoint;
    [SerializeField] TMP_Text foodPoint;

    public void ShowHealthPoint()
    {
        int hpLevel = PlayerStatManager.Inventory.hpLevel;

        medicalPoint.text = (PlayerStatManager.Inventory.statLevelUpPoint[hpLevel].medicalPoint).ToString();
        foodPoint.text = (PlayerStatManager.Inventory.statLevelUpPoint[hpLevel].foodPoint).ToString();
    }

    public void ShowStaminaPoint()
    {
        int staminaLevel = PlayerStatManager.Inventory.staminaLevel;

        medicalPoint.text = (PlayerStatManager.Inventory.statLevelUpPoint[staminaLevel].medicalPoint).ToString();
        foodPoint.text = (PlayerStatManager.Inventory.statLevelUpPoint[staminaLevel].foodPoint).ToString();
    }

    public void ShowSpeedPoint()
    {
        int speedLevel = PlayerStatManager.Inventory.speedLevel;

        medicalPoint.text = (PlayerStatManager.Inventory.statLevelUpPoint[speedLevel].medicalPoint).ToString();
        foodPoint.text = (PlayerStatManager.Inventory.statLevelUpPoint[speedLevel].foodPoint).ToString();
    }
}
