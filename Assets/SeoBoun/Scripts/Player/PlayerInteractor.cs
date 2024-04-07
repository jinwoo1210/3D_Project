using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] LayerMask itemLayer;
    Collider[] colliders = new Collider[10];
    private void OnInteract(InputValue value)
    {
        Debug.Log("��ȣ�ۿ� �õ�");
        int size = Physics.OverlapSphereNonAlloc(transform.position, 3f, colliders, itemLayer);
        
        for(int i = 0; i < size; i++)
        {
            IInteractable target = colliders[i].gameObject.GetComponent<IInteractable>();
            if(target != null)
            {
                target.Interactor(this);
                return;
            }
        }
    }
}
