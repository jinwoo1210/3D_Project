using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMedicalSlider : MonoBehaviour
{
    [SerializeField] TMP_Text sliderValue;
    [SerializeField] Slider slider;

    public void ChangeValue()
    {
        if (PlayerStatManager.Inventory.medicalPoint < slider.value)
        {
            return;
        }

        sliderValue.text = $"{slider.value} / 3";
        PlayerStatManager.Inventory.FieldInventory.MedicalPoint = (int)slider.value;
    }
}
