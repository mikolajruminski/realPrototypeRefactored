using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyBehaviourScript : Item
{
    public static event EventHandler OnAnyKeyPicked;
    public event EventHandler OnInteractDetected;
    public event EventHandler OnInteractableEnded;

    public static void ResetStaticData()
    {
        OnAnyKeyPicked = null;
    }

    public override void Interact()
    {
        if (!InventoryManager.Instance.inventoryItems.Contains(allItemsSO.ItemType))
        {
            InventoryManager.Instance.AddItems(allItemsSO.ItemType);
            KeysUIScript.Instance.AdjustKeys();
            Player.Instance.ClearLastInteractable();
            OnAnyKeyPicked?.Invoke(this, EventArgs.Empty);
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
