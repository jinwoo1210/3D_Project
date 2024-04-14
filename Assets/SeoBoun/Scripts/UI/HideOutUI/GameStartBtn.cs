using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartBtn : MonoBehaviour
{
    [SerializeField] TMP_Text timeText; 
    public void StartBtn()
    {
        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        float time = 0f;

        while (time < 5f)
        {
            time += Time.deltaTime;
            timeText.text = $"{time:F3}";

            yield return null;
        }
        Manager.Scene.GetCurScene().GetComponent<HideScene>().ExitScene();
    }
}
