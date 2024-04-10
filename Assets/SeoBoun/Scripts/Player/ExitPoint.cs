using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ż�ⱸ ����");
        Manager.Scene.LoadScene("HideScene");
        if (((1 << playerLayer) & other.gameObject.layer) != 0)
        {
            Debug.Log("�� �̵�");
            Manager.Scene.LoadScene("HideScene");
        }
    }
}
