using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : BaseItem
{   
    public int value;

    private void Update()
    {
        transform.Rotate(Vector3.up * 30 *  Time.deltaTime, Space.World);
    }
}
