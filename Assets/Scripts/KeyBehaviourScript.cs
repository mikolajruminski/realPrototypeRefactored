using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyBehaviourScript : Item
{
    public event EventHandler OnInteractDetected;
    public event EventHandler OnInteractableEnded;

    public override void Interact()
    {
        if (!InventoryManager.Instance.inventoryItems.Contains(allItemsSO.ItemType))
        {
            InventoryManager.Instance.AddItems(allItemsSO.ItemType);
            KeysUIScript.Instance.AdjustKeys();
            Player.Instance.ClearLastInteractable();
            Destroy(gameObject);

        }
    }

    public override void ShowInteractableButton()
    {
        OnInteractDetected.Invoke(this, EventArgs.Empty);
        isInteractable = true;
    }

    public override void HideInteractableButton()
    {
        OnInteractableEnded.Invoke(this, EventArgs.Empty);
        isInteractable = false;
    }
}
