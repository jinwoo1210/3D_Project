using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMedicalSlider : MonoBehaviour
{
    [SerializeField] TMP_Text sliderValue;
    [SerializeField] Slider slider;

    int maxPoint;

    public void SetMaxValue()
    {
        maxPoint = PlayerStatManager.Inventory.medicalPoint < 3 ? PlayerStatManager.Inventory.medicalPoint : 3;
    }

    public void ChangeValue()
    {
        if (maxPoint < slider.value)
        {
            slider.value = maxPoint;
            return;
        }

        sliderValue.text = $"{slider.value} / 3";
        PlayerStatManager.Inventory.FieldInventory.MedicalPoint = (int)slider.value;
    }
}
