using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBackButton : MonoBehaviour
{
    public void BackButton()
    {
        Manager.Scene.LoadScene("ItemScene");
    }
}
