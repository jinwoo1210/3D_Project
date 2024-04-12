using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItemDrop : MonoBehaviour
{
    [SerializeField] BaseItem dropItem;

    public void Drop()
    {
        int rand = Random.Range(0, 100);

        if(rand > 89)
        {
            Instantiate(dropItem, transform.position, transform.rotation);
        }
    }
}
