using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighLightBtnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TMP_Text highlightText;
    [SerializeField] Image highlightImage;
    [SerializeField] Color normalColor;
    [SerializeField] Color highlightColor;

    private void OnEnable()
    {
        if (highlightText != null)
            highlightText.color = normalColor;
        if (highlightImage != null)
            highlightImage.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highlightText != null)
            highlightText.color = highlightColor;
        if (highlightImage != null)
            highlightImage.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (highlightText != null)
            highlightText.color = normalColor;
        if (highlightImage != null)
            highlightImage.color = normalColor;
    }
}
