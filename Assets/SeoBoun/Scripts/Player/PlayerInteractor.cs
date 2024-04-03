using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [ContextMenu("Interactor")]
    private void OnInteract()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
        
        foreach(Collider collider in colliders)
        {
            IInteractable target = collider.gameObject.GetComponent<IInteractable>();
            if(target != null)
            {
                target.Interactor(this);
            }
        }
    }
}
