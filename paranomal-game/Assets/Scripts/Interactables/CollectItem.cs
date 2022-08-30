using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : Interactable
{
    protected override void Interact()
    {
        Destroy(gameObject);
    }
}
